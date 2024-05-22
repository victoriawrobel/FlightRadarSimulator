using FlightTrackerGUI;
using OOD_24L_01180686.source.Visualization;
using OOD_24L_01180686.source.Network;
using OOD_24L_01180686.source.Reports;
using OOD_24L_01180686.source.Writers;
using OOD_24L_01180686.source.Commands;

namespace OOD_24L_01180686.source
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
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
            var file2 = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example.ftre";
            IDataWrite dataWrite = new JSONWriter();

            EntitySearch.AddReporter(new Newspaper("Polytechnical Gazette"));
            EntitySearch.AddReporter(new Newspaper("Categories Journal"));

            EntitySearch.AddReporter(new Television("Abelian Television "));
            EntitySearch.AddReporter(new Television("Channel TV-Tensor "));

            EntitySearch.AddReporter(new Radio("Quantifier radio"));
            EntitySearch.AddReporter(new Radio("Shmem radio"));


            Server.GetInstance(file);
            UpdateServer.GetInstance(file2);
            FlightGUIDataClass flightGUIData = new FlightGUIDataClass();
            var flightGUIThread = new FlightGUIThread(flightGUIData);
            flightGUIThread.Start();

            UpdateServer.GetInstance(file2).StartServer();
            CommandHandlerClass.CommandHandler(Server.GetInstance(file));

            flightGUIThread.Stop();
            UpdateServer.GetInstance(file2).StopServer();
            thread.Interrupt();
            Console.WriteLine("Main thread exited.");
        }
    }
}