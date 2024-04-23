using System.Xml.Serialization;
using NetworkSourceSimulator;
using OOD_24L_01180686.source.Objects;
using OOD_24L_01180686.source.Reports;
using OOD_24L_01180686.source.Updates;

namespace OOD_24L_01180686.source
{
    internal class EntitySearch : IObserver
    {
        internal static Dictionary<ulong, Entity> EntitySearchDictionary = new Dictionary<ulong, Entity>();
        private static List<object> Objects = new List<object>();
        internal static object lockObject = new object();

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

        public void Update(IDUpdateArgs args)
        {
            if (EntitySearchDictionary.ContainsKey(args.NewObjectID) || !EntitySearchDictionary.ContainsKey(args.ObjectID))
            {
                return;
            }
            var obj = GetObject(args.ObjectID) as Entity;
            if (obj != null)
            {
                lock (lockObject)
                {
                    var newObj = EntitySearchDictionary[args.ObjectID];
                    EntitySearchDictionary.Remove(args.ObjectID);
                    newObj.ID = args.NewObjectID;
                    EntitySearchDictionary.Add(args.NewObjectID, newObj);
                }
            }
        }

        public void Update(PositionUpdateArgs args)
        {
            Entity entity = null;
            if (EntitySearchDictionary.TryGetValue(args.ObjectID, out entity))
            {
                Flight flight = entity as Flight;
                if (flight != null)
                {
                    lock (lockObject)
                    {
                        flight.Longitude = args.Longitude;
                        flight.Latitude = args.Latitude;
                        flight.AMSL = args.AMSL;
                    }
                }
            }
        }

        public void Update(ContactInfoUpdateArgs args)
        {
            Entity entity = null;
            if (EntitySearchDictionary.TryGetValue(args.ObjectID, out entity))
            {
                Person person = entity as Person;
                if (person != null)
                {
                    lock (lockObject)
                    {
                        person.Phone = args.PhoneNumber;
                        person.Email = args.EmailAddress;
                    }
                }
            }
        }

    }
}