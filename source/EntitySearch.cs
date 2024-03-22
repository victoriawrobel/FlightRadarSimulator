using OOD_24L_01180686.source.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source
{
    internal interface EntitySearch
    {
        private static readonly Dictionary<ulong, Entity> EntitySearchDictionary = new Dictionary<ulong, Entity>();

        public static void AddObject(Entity e)
        {
            EntitySearchDictionary.Add(e.ID, e);
            Console.WriteLine($"Added object with ID {e.ID} to EntitySearchDictionary.");
        }

        public static object GetObject(ulong ID)
        {
            if (EntitySearchDictionary.TryGetValue(ID, out var entity))
            {
                return entity;
            }
            throw new KeyNotFoundException($"Object with ID {ID} not found.");
        }
    }
}
