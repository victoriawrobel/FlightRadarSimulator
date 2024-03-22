using OOD_24L_01180686.source.Objects;
using FlightTrackerGUI;
using OOD_24L_01180686.source.Adapters;
using OOD_24L_01180686.source.ObjectsCollection;

namespace OOD_24L_01180686.source.Visualization
{
    public class FlightGUIDataClass : FlightsGUIData
    {
        private List<FlightGUI> flightsData;

        public FlightGUIDataClass(List<FlightGUI> flightsData)
        {
            this.flightsData = flightsData;
        }

        public static void UpdateFlightsGUI()
        {
            List<FlightGUI> flightGUIs = new List<FlightGUI>();

            foreach (var obj in ObjectsCollection.ObjectsCollection.GetObjects())
            {
                if (obj is Flight flight)
                {
                    flight.UpdatePosition();
                    if(flight.GetProgress() < 1 && flight.GetProgress() > 0)
                        flightGUIs.Add(FlightToFlightGUIAdapter.FLightGUIAdapter(flight));
                }
            }

            FlightsGUIData flightsGUIData = new FlightGUIDataClass(flightGUIs);
            Runner.UpdateGUI(flightsGUIData);
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
