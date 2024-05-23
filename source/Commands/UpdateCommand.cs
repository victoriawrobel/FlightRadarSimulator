using OOD_24L_01180686.source.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal class UpdateCommand : Command
    {
        public Dictionary<string, string> KeyValuePairs { get; }
        public string Conditions { get; }

        public UpdateCommand(string objectClass, Dictionary<string, string> keyValuePairs, string conditions) : base(
            objectClass)
        {
            KeyValuePairs = keyValuePairs;
            Conditions = conditions;
        }

        public override void Execute()
        {
            var objects = FetchObjects(ObjectClass);
            var filteredObjects = FilterObjects(objects, Conditions);
            UpdateFields(filteredObjects, KeyValuePairs);
        }

        private void UpdateFields(IEnumerable<object> objects, Dictionary<string, string> keyValuePairs)
        {
            lock (EntitySearch.lockObject)
            {
                foreach (var obj in objects)
                {
                    var entity = obj as Entity;
                    if (entity != null)
                    {
                        foreach (var kvp in keyValuePairs)
                        {
                            var fieldName = kvp.Key;
                            var newValue = ParseValue(kvp.Value);

                            if (fieldName.Equals("ID", StringComparison.OrdinalIgnoreCase))
                            {
                                EntitySearch.Update(entity.ID, (ulong)newValue);
                            }

                            entity.FieldMap[fieldName] = () => newValue;
                        }

                        EntitySearch.EntitySearchDictionary[entity.ID] = entity;
                    }
                }
            }
        }
    }
}