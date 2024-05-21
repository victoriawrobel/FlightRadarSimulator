using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Commands
{
    internal class DisplayCommand : Command
    {
        public string[] ObjectFields { get; }
        public string Conditions { get; }

        public DisplayCommand(string[] objectFields, string objectClass, string conditions) : base(objectClass)
        {
            ObjectFields = objectFields;
            Conditions = conditions;
        }

        public override void Execute()
        {
            var objects = FetchObjects(ObjectClass);

            var filteredObjects = FilterObjects(objects, Conditions);

            var fieldData = SelectFields(filteredObjects, ObjectFields);

            DisplayTable(fieldData);
        }

        private IEnumerable<object> FetchObjects(string objectClass)
        {
            lock (EntitySearch.lockObject)
            {
                return EntitySearch.EntitySearchDictionary.Values
                    .Where(entity => entity.GetTypeCustom().Equals(objectClass, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        private IEnumerable<object> FilterObjects(IEnumerable<object> objects, string conditions)
        {
            if (string.IsNullOrWhiteSpace(conditions)) return objects;

            var filteredObjects = new List<object>();
            var operators = new[] { ">=", "<=", "==", "!=", "=", ">", "<" };

            var conditionParts = Regex.Split(conditions, @"\s*(and|or)\s*")
                                       .Where(part => !string.IsNullOrWhiteSpace(part))
                                       .ToList();

            foreach (var obj in objects)
            {
                var entity = obj as Entity;
                if (entity == null) continue;

                var resultsStack = new Stack<bool>();
                var logicStack = new Stack<string>();

                for (int i = 0; i < conditionParts.Count; i++)
                {
                    var part = conditionParts[i].Trim();

                    if (part.Equals("and", StringComparison.OrdinalIgnoreCase) || part.Equals("or", StringComparison.OrdinalIgnoreCase))
                    {
                        logicStack.Push(part.ToLower());
                    }
                    else
                    {
                        string fieldName = null;
                        string operatorUsed = null;
                        string value = null;

                        foreach (var op in operators)
                        {
                            var parts = part.Split(new[] { op }, 2, StringSplitOptions.None);
                            if (parts.Length == 2)
                            {
                                fieldName = parts[0].Trim();
                                operatorUsed = op;
                                value = parts[1].Trim();
                                break;
                            }
                        }

                        var propertyValue = entity.GetFieldValue(fieldName);
                        if (propertyValue == null)
                        {
                            resultsStack.Push(false);
                        }
                        else
                        {
                            var parsedPropertyValue = ParseValue(propertyValue.ToString());
                            var parsedValue = ParseValue(value);

                            if (Operators.TryGetValue(operatorUsed, out var opFunc))
                            {
                                resultsStack.Push(opFunc(parsedPropertyValue, parsedValue));
                            }
                            else
                            {
                                resultsStack.Push(false);
                            }
                        }

                        if (resultsStack.Count > 1 && logicStack.Count > 0)
                        {
                            var right = resultsStack.Pop();
                            var left = resultsStack.Pop();
                            var logicOp = logicStack.Pop();

                            if (logicOp == "and")
                            {
                                resultsStack.Push(left && right);
                            }
                            else if (logicOp == "or")
                            {
                                resultsStack.Push(left || right);
                            }
                        }
                    }
                }

                if (resultsStack.Count == 1 && resultsStack.Pop())
                {
                    filteredObjects.Add(obj);
                }
            }

            return filteredObjects;
        }


        private IEnumerable<Dictionary<string, object>> SelectFields(IEnumerable<object> objects, string[] fields)
        {
            var fieldData = new List<Dictionary<string, object>>();

            foreach (var obj in objects)
            {
                var entity = obj as Entity;
                var fieldDict = new Dictionary<string, object>();
                foreach (var field in fields)
                {
                    var propertyValue = entity.GetFieldValue(field);
                    fieldDict[field] = propertyValue;
                }
                fieldData.Add(fieldDict);
            }

            return fieldData;
        }

        private void DisplayTable(IEnumerable<Dictionary<string, object>> fieldData)
        {
            var headers = ObjectFields;
            var rows = fieldData.Select(dict => headers.Select(h => dict[h]?.ToString() ?? "").ToArray()).ToArray();

            var columnWidths = headers.Select((h, i) => Math.Max(h.Length, rows.Max(row => row[i].Length))).ToArray();

            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write("| " + headers[i].PadRight(columnWidths[i]) + " ");
            }
            Console.WriteLine("|");

            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write("|-" + new string('-', columnWidths[i]) + "-");
            }
            Console.WriteLine("|");

            foreach (var row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    Console.Write("| " + row[i].PadRight(columnWidths[i]) + " ");
                }
                Console.WriteLine("|");
            }
        }

    }
}
