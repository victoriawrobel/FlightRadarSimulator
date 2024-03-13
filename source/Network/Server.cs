using NetworkSourceSimulator;
using OOD_24L_01180686.source.Readers;
using System.Text;

namespace OOD_24L_01180686.source.Network
{
    public class Server
    {
        private NetworkSourceSimulator.NetworkSourceSimulator server;
        private static Server serverInstance;
        public static List<object> Objects = new List<object>();
        internal static bool IsRunning = false;
        private static object serverLock = new object();
        private string Filepath;
        private int MaxDelay = 100;
        private int MinDelay = 10;

        private Server(string filepath)
        {
            Filepath = filepath;
        }

        public static Server GetInstance(string filepath)
        {
            lock (serverLock)
            {
                if (serverInstance == null)
                {
                    serverInstance = new Server(filepath);
                }
            }

            return serverInstance;
        }

        public void StartServer()
        {
            if (!File.Exists(Filepath))
            {
                throw new FileNotFoundException($"File {Filepath} not found");
            }

            IsRunning = true;
            Console.WriteLine("Server started.");
            server = new NetworkSourceSimulator.NetworkSourceSimulator(Filepath, MinDelay, MaxDelay);
            server.OnNewDataReady += ServerOnNewDataReady;
            Task.Run(() => server.Run());
        }

        public Task StopServer()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Console.WriteLine("Server stopping...");
            }

            return Task.CompletedTask;
        }

        private void ServerOnNewDataReady(object sender, NewDataReadyArgs e)
        {
            var message = server.GetMessageAt(e.MessageIndex);
            Objects.Add(MessageParser(message));
        }

        private static object MessageParser(Message message)
        {
            var objectType = Encoding.Default.GetString(message.MessageBytes, 0, 3);
            if (Reader.objectCreatorsFromMessages.TryGetValue(objectType, out var creator))
            {
                return creator(message.MessageBytes);
            }

            throw new ArgumentException("Unrecognized object type.");
        }
    }
}