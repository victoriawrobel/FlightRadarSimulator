using FlightTrackerGUI;
using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Adapters
{
    public static class FlightToFlightGUIAdapter
    {
        public static FlightGUI FLightGUIAdapter(Flight flight)
        {
            WorldPosition worldPosition = new WorldPosition(flight.Latitude, flight.Longitude);
            return new FlightGUI { ID = flight.ID, WorldPosition = worldPosition, MapCoordRotation = flight.GetRotation() };
        }   
    }
}
