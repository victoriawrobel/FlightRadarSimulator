using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.ObjectsCollection
{
    public static class ObjectsCollection
    {
        private static List<object> Objects = new List<object>();
        private static object lockObject = new object();

        public static void AddObject(object obj)
        {
            lock(lockObject)
            {
                Objects.Add(obj);
            }
        }

        public static List<object> GetObjects()
        {
            lock(lockObject)
            {
                return Objects;
            }
        }
    }
}
