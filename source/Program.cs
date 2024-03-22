using FlightTrackerGUI;
using OOD_24L_01180686.source.FlightGUI_Implement;
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

            Thread updater = new Thread(() =>
            {
                while (true)
                {
                    FlightGUIDataClass.UpdateFlightsGUI();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            });
            updater.Start();
            var file = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr";
            IDataWrite dataWrite = new JSONWriter();
            Server.GetInstance(file);

            CommandHandlerClass.CommandHandler(Server.GetInstance(file));

            

            Console.WriteLine("Main thread exited.");
        }
    }
}