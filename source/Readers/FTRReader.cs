using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180686.src.Objects;

namespace OOD_24L_01180686.src.Readers
{
    public interface IDataRead
    {
        IEnumerable<object> ReadData(string filepath);
    }

    public class FTRReader : IDataRead
    {
        private Dictionary<string, Func<string[], object>> objectCreators = new Dictionary<string, Func<string[], object>>
        {
            { "C", attributes => new Crew(UInt64.Parse(attributes[1]), attributes[2], UInt64.Parse(attributes[3]), attributes[4], attributes[5], UInt16.Parse(attributes[6]), attributes[7]) },
            { "P", attributes => new Passenger(UInt64.Parse(attributes[1]), attributes[2], UInt64.Parse(attributes[3]), attributes[4], attributes[5], attributes[6], UInt64.Parse(attributes[7])) },
            { "CA", attributes => new Cargo(UInt64.Parse(attributes[1]), Single.Parse(attributes[2]), attributes[3], attributes[4]) },
            { "CP", attributes => new CargoPlane(UInt64.Parse(attributes[1]), attributes[2], attributes[3], attributes[4], Single.Parse(attributes[5])) },
            { "PP", attributes => new PassengerPlane(UInt64.Parse(attributes[1]), attributes[2], attributes[3], attributes[4], UInt16.Parse(attributes[5]), UInt16.Parse(attributes[6]), UInt16.Parse(attributes[7])) },
            { "AI", attributes => new Airport(UInt64.Parse(attributes[1]), attributes[2], attributes[3], Single.Parse(attributes[4]), Single.Parse(attributes[5]), Single.Parse(attributes[6]), attributes[7]) },
            { "FL", attributes =>
                {
                    var crewIDs = attributes[10].Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
                    var loadIDs = attributes[11].Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
                    return new Flight(UInt64.Parse(attributes[1]), UInt64.Parse(attributes[2]), UInt64.Parse(attributes[3]), attributes[4], attributes[5], Single.Parse(attributes[6]),
                        Single.Parse(attributes[7]), Single.Parse(attributes[8]), UInt64.Parse(attributes[9]), crewIDs, loadIDs);
                }
            }
        };

        public IEnumerable<object> ReadData(string filepath)
        {
            if (!filepath.EndsWith(".ftr"))
                throw new ArgumentException("Wrong file type.");

            List<object> objects = new List<object>();
            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var attr = reader.ReadLine().Split(',');
                    objects.Add(LineParser(attr));
                }

            }
            return objects;
        }

        private object LineParser(string[] attr)
        {
            string objectType = attr[0];
            if (objectCreators.TryGetValue(objectType, out var creator))
            {
                return creator(attr);
            }
            throw new ArgumentException("Unrecognized object type.");
        }

    }


}
