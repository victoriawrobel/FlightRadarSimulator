using OOD_24L_01180686.source.Factories;
using OOD_24L_01180686.source.ServerActions;
using OOD_24L_01180686.source.Network;
using OOD_24L_01180686.source.Writers;
using System.Data;


namespace OOD_24L_01180686.source
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string file = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr";
            IDataWrite dataWrite = new JSONWriter();
            Server server = new Server(file);

            CommandHandlerClass.CommandHandler(server);

            Console.WriteLine("Main thread exited.");
        }
    }
}