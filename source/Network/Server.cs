using NetworkSourceSimulator;
using OOD_24L_01180686.source.Writers;

namespace OOD_24L_01180686.source.ServerActions
{
    public class Server
    {
        private readonly IDataWrite DataWrite;
        private NetworkSourceSimulator.NetworkSourceSimulator server;
        public static bool IsRunning = false;
        public static string Filepath;
        public static int MaxDelay = 1000;
        public static int MinDelay = 100;

        public Server(string filepath, IDataWrite dataWrite)
        {
            Filepath = filepath;
            this.DataWrite = dataWrite;
        }
        
        public async Task StartServer()
        {
            if(!File.Exists(Filepath))
            {
                throw new FileNotFoundException($"File {Filepath} not found");
            }

            IsRunning = true;
            Console.WriteLine("Server started.");
            server = new NetworkSourceSimulator.NetworkSourceSimulator(Filepath, MinDelay, MaxDelay);
            server.OnNewDataReady += ServerOnNewDataReady;
            server.Run();

        }

        public async Task StopServer()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Console.WriteLine("Server stopping...");
            }
        }

        public static void ServerOnNewDataReady(object sender, NewDataReadyArgs e)
        {
            Console.WriteLine("New data ready: " + e.MessageIndex); //TODO: expand the functionality
        }

        
        public static void CreateSnapshot()
        {
            //string snapshotFileName = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\" + $"snapshot_{DateTime.Now:HH_mm_ss}.json";
            //var data = GetData();
            //DataWrite.WriteData(data, snapshotFileName);
            //Console.WriteLine($"Snapshot saved to {snapshotFileName}"); //TODO : fix accessibility
        }

        public static IEnumerable<object> GetData()
        {
            return null; //TODO : implement
        }
    }
}
