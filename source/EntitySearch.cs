using OOD_24L_01180686.source.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180686.source.Reports;

namespace OOD_24L_01180686.source
{
    internal interface EntitySearch
    {
        private static readonly Dictionary<ulong, Entity> EntitySearchDictionary = new Dictionary<ulong, Entity>();
        private static List<object> Objects = new List<object>();
        private static object lockObject = new object();

        private static LinkedList<Flight> flightList = new LinkedList<Flight>();
        private static LinkedList<Reporter> reporterList = new LinkedList<Reporter>();
        private static LinkedList<IReportable> reportableList = new LinkedList<IReportable>();

        public static void AddReporter(Reporter r)
        {
            lock (lockObject)
            {
                reporterList.AddLast(r);
            }
        }

        public static void AddReportable(IReportable r)
        {
            lock (lockObject)
            {
                reportableList.AddLast(r);
            }
        }

        public static LinkedList<Reporter> GetReporters()
        {
            lock (lockObject)
            {
                return reporterList;
            }
        }

        public static LinkedList<IReportable> GetReportables()
        {
            lock (lockObject)
            {
                return reportableList;
            }
        }

        public static void AddObject(Entity e)
        {
            lock (lockObject)
            {
                EntitySearchDictionary.Add(e.ID, e);
            }
        }

        public static void AddFlight(Flight f)
        {
            lock (lockObject)
            {
                flightList.AddLast(f);
            }
        }

        public static LinkedList<Flight> GetFlights()
        {
            lock (lockObject)
            {
                return flightList;
            }
        }

        public static object GetObject(ulong ID)
        {
            lock (lockObject)
            {
                if (EntitySearchDictionary.TryGetValue(ID, out var entity))
                {
                    return entity;
                }

                throw new KeyNotFoundException($"Object with ID {ID} not found.");
            }
        }

        public static void AddObject(object obj)
        {
            lock (lockObject)
            {
                Objects.Add(obj);
            }
        }

        public static List<object> GetObjects()
        {
            lock (lockObject)
            {
                return Objects;
            }
        }
    }
}