using OOD_24L_01180686.source.Objects;
using FlightTrackerGUI;
using OOD_24L_01180686.source.Adapters;

namespace OOD_24L_01180686.source.Visualization
{
    public class FlightGUIDataClass : FlightsGUIData
    {
        private static List<FlightGUI> flightsData { get; set; }

        public FlightGUIDataClass(List<FlightGUI> FlightsData)
        {
            flightsData = FlightsData;
        }

        public static void UpdateFlightsGUI()
        {
            flightsData.Clear();
            foreach (var obj in ObjectsCollection.ObjectsCollection.GetObjects())
            {
                if (obj is Flight flight)
                {
                    flight.UpdatePosition();
                    if (flight.GetProgress() < 1 && flight.GetProgress() > 0)
                    {
                        Console.WriteLine("Flight ID: " + flight.ID + " is in progress.");
                        flightsData.Add(new FlightGUIAdapter(flight));
                    }
                }
            }
            Console.WriteLine("flightData count: " + flightsData.Count);
        }

        public override int GetFlightsCount()
        {
            return flightsData.Count;
        }

        
        public override ulong GetID(int index)
        {
            if (index >= 0 && index < flightsData.Count)
            {
                return flightsData[index].ID;
            }
            else
            {
                return 0;
            }
        }


        public override WorldPosition GetPosition(int index)
        {
            if (index >= 0 && index < flightsData.Count)
            {
                return flightsData[index].WorldPosition;
            }
            else
            {
                return new WorldPosition();
            }
        }

        public override double GetRotation(int index)
        {
            if (index >= 0 && index < flightsData.Count)
            {
                return flightsData[index].MapCoordRotation;
            }
            else
            {
                return 0;
            }
        }

    }
}
