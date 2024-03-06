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
            //IReaderFactory fileReaderFactory = new FTRReaderFactory();
            //var fileReader = fileReaderFactory.Create();
            //Console.WriteLine("Deserializing...");
            //var list = fileReader.ReadData(Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr");
            //Console.WriteLine("Deserialization complete.");

            //IWriterFactory fileWriterFactory = new JSONWriterFactory();
            //var fileWriter = fileWriterFactory.Create();
            //Console.WriteLine("Serializing...");
            //fileWriter.WriteData(list, Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1serializer.json");
            //Console.WriteLine("Serialization complete.");

            string file = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr";
            IDataWrite dataWrite = new JSONWriter();
            Server server = new Server(file, dataWrite);

            CommandHandlerClass.CommandHandler(server);

            Console.WriteLine("Main thread exited.");
        }
    }
}