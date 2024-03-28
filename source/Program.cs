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
            Thread thread = new Thread(() =>
            {
                try
                {
                    Runner.Run();
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Runner thread interrupted.");
                }
            });
            thread.Start();
            var file = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr";
            IDataWrite dataWrite = new JSONWriter();
            Server.GetInstance(file);
            FlightGUIDataClass flightGUIData = new FlightGUIDataClass();
            var flightGUIThread = new FlightGUIThread(flightGUIData);
            flightGUIThread.Start();

            CommandHandlerClass.CommandHandler(Server.GetInstance(file));
            flightGUIThread.Stop();

            thread.Interrupt();
            Console.WriteLine("Main thread exited.");
        }
    }
}