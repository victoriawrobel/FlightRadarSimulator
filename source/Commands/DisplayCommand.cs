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


        private void DisplayTable(IEnumerable<Dictionary<string, object>> fieldData)
        {
            var headers = fieldData.FirstOrDefault()?.Keys.ToArray();
            if (headers == null)
            {
                Console.WriteLine("No data to display.");
                return;
            }

            var rows = fieldData.Select(dict => headers.Select(h => GetFormattedValue(dict[h])).ToArray()).ToArray();

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
                    Console.Write("| " + row[i].PadLeft(columnWidths[i]) + " ");
                }
                Console.WriteLine("|");
            }
        }

        private string GetFormattedValue(object value)
        {
            if (value == null)
                return "";

            if (value as IEnumerable<object> != null)
            {
                IEnumerable<object> enumerable = value as IEnumerable<object>;
                var arrayValues = enumerable.Select(item => item?.ToString() ?? "");
                return string.Join(", ", arrayValues);
            }

            if (value as IEnumerable<ulong> != null)
            {
                IEnumerable<ulong> ulongEnumerable = value as IEnumerable<ulong>;

                var arrayValues = ulongEnumerable.Select(item => item.ToString());
                return string.Join(", ", arrayValues);
            }

            return value.ToString();
        }

        private IEnumerable<Dictionary<string, object>> SelectFields(IEnumerable<object> objects, string[] fields)
        {
            var fieldData = new List<Dictionary<string, object>>();
            bool all = fields.Length == 1 && fields[0] == "*";
            foreach (var obj in objects)
            {
                var entity = obj as Entity;
                if (all) fields = entity.FieldMap.Keys.ToArray();
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


    }
}