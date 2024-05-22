using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180686.source.Readers;

namespace OOD_24L_01180686.source.Commands
{
    internal class AddCommand : Command
    {
        public Dictionary<string, string> KeyValuePairs { get; }

        public AddCommand(string objectClass, Dictionary<string, string> keyValuePairs) : base(objectClass)
        {
            KeyValuePairs = keyValuePairs;
        }

        public override void Execute()
        {
            if (Reader.objectCreatorsAdd.TryGetValue(ObjectClass, out var fun))
            {
                var ent = fun.Invoke();
                EntitySearch.AddObject(ent);
                foreach (var kvp in KeyValuePairs)
                {
                    var fieldName = kvp.Key;
                    var newValue = ParseValue(kvp.Value);

                    if (fieldName.Equals("ID", StringComparison.OrdinalIgnoreCase))
                    {
                        EntitySearch.Update(ent.ID, (ulong)newValue);
                    }

                    ent.FieldMap[fieldName] = () => newValue;
                }
            }
        }
    }
}
