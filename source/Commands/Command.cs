using OOD_24L_01180686.source.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal abstract class Command
    {
        public static readonly Dictionary<string, Func<IComparable, IComparable, bool>> Operators =
            new Dictionary<string, Func<IComparable, IComparable, bool>>
            {
                { "==", (a, b) => a.Equals(b) },
                { "!=", (a, b) => !a.Equals(b) },
                { ">", (a, b) => a.CompareTo(b) > 0 },
                { "<", (a, b) => a.CompareTo(b) < 0 },
                { ">=", (a, b) => a.CompareTo(b) >= 0 },
                { "<=", (a, b) => a.CompareTo(b) <= 0 }
            };

        public Command()
        {
        }

        public Command(string objectClass)
        {
            ObjectClass = objectClass;
        }

        protected string ObjectClass { get; set; }
        public abstract void Execute();

        protected IComparable ParseValue(string value)
        {
            if (ulong.TryParse(value, out var intValue))
            {
                return intValue;
            }

            if (float.TryParse(value, out var doubleValue))
            {
                return doubleValue;
            }

            return value;
        }

        protected IEnumerable<object> FetchObjects(string objectClass)
        {
            lock (EntitySearch.lockObject)
            {
                return EntitySearch.EntitySearchDictionary.Values
                    .Where(entity => entity.GetTypeCustom().Equals(objectClass, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        protected IEnumerable<object> FilterObjects(IEnumerable<object> objects, string conditions)
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

                    if (part.Equals("and", StringComparison.OrdinalIgnoreCase) ||
                        part.Equals("or", StringComparison.OrdinalIgnoreCase))
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
    }
}