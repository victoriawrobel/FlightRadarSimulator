using FlightTrackerGUI;
using OOD_24L_01180686.source.Visualization;
using OOD_24L_01180686.source.Network;
using OOD_24L_01180686.source.Writers;

namespace OOD_24L_01180686.source
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Thread thread = new Thread(() => Runner.Run());
            thread.Start();
            var file = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr";
            IDataWrite dataWrite = new JSONWriter();
            Server.GetInstance(file);
            FlightGUIDataClass flightGUIData = new FlightGUIDataClass();
            Thread updater = new Thread(() =>
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
            }
            );
            updater.Start();

            CommandHandlerClass.CommandHandler(Server.GetInstance(file));
            updater.Interrupt();

            Console.WriteLine("Main thread exited.");
        }
    }
}