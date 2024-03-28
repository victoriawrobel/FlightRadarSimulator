using FlightTrackerGUI;
using OOD_24L_01180686.source.Visualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source
{
    internal class FlightGUIThread
    {
        private readonly FlightGUIDataClass flightGUIData;
        private Thread updater;

        public FlightGUIThread(FlightGUIDataClass flightGUIData)
        {
            this.flightGUIData = flightGUIData;
        }

        public void Start()
        {
            updater = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        FlightGUIDataClass.UpdateFlightsGUI();
                        Runner.UpdateGUI(flightGUIData);
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                    catch (ThreadInterruptedException)
                    {
                        Console.WriteLine("FlightGUI updater interrupted.");
                        break;
                    }
                }
            });

            updater.Start();
        }

        public void Stop()
        {
            updater.Interrupt();
        }
    }
}