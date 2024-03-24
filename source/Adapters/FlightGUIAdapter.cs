using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Adapters
{
    public class FlightGUIAdapter : FlightGUI
    {
        private Flight flight;
        public FlightGUIAdapter(Flight flight)
        {
            this.flight = flight;
            this.WorldPosition = new WorldPosition(flight.Latitude, flight.Longitude);
            this.ID = flight.ID;
            this.MapCoordRotation = flight.GetRotation();
        }
    }
}
