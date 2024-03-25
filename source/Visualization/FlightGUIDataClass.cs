﻿using OOD_24L_01180686.source.Objects;
using FlightTrackerGUI;

namespace OOD_24L_01180686.source.Visualization
{
    public class FlightGUIDataClass : FlightsGUIData
    {
        private static List<Flight> flightsData = new List<Flight>();

        public FlightGUIDataClass()
        {
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
                        flightsData.Add(flight);
                    }
                }
            }
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
                return new WorldPosition(flightsData[index].Latitude, flightsData[index].Longitude);
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
                return flightsData[index].GetRotation();
            }
            else
            {
                return 0;
            }
        }

    }
}
