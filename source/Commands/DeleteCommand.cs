using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal class DeleteCommand : Command
    {
        public string ObjectClass { get; }
        public string Conditions { get; }

        public DeleteCommand(string objectClass, string conditions) : base(objectClass)
        {
            ObjectClass = objectClass;
            Conditions = conditions;
        }

        public override void Execute()
        {
            var objects = FetchObjects(ObjectClass);

            var filteredObjects = FilterObjects(objects, Conditions);

            foreach (var obj in filteredObjects)
            {
                EntitySearch.deleteObject(obj);
            }

            foreach (var flight in EntitySearch.GetFlights())
            {
                flight.CrewID = flight.CrewID.Where(id => !filteredObjects.Contains(id)).ToArray();
                flight.LoadID = flight.LoadID.Where(id => !filteredObjects.Contains(id)).ToArray();
            }
        }
    }
}
