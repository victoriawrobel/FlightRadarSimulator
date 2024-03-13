using OOD_24L_01180686.source.Network;
using OOD_24L_01180686.source.Writers;

namespace OOD_24L_01180686.source
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var file = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr";
            IDataWrite dataWrite = new JSONWriter();
            Server.GetInstance(file);

            CommandHandlerClass.CommandHandler(Server.GetInstance(file));

            Console.WriteLine("Main thread exited.");
        }
    }
}