using NetworkSourceSimulator;
using OOD_24L_01180686.source.Readers;
using OOD_24L_01180686.source.Writers;
using System.Text;

namespace OOD_24L_01180686.source.ServerActions
{
    public class Server
    {
        private static NetworkSourceSimulator.NetworkSourceSimulator server;
        public static List<object> Objects = new List<object>();
        public static bool IsRunning = false;
        public static string Filepath;
        public static int MaxDelay = 1000;
        public static int MinDelay = 100;

        public Server(string filepath)
        {
            Filepath = filepath;
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
            Task.Run(() => server.Run());

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
            Message message = server.GetMessageAt(e.MessageIndex);
            Objects.Add(MessageParser(message));
            
        }

        private static object MessageParser(Message message)
        {
            string objectType = Encoding.Default.GetString(message.MessageBytes, 0, 3);
            if (Reader.objectCreatorsFromMessages.TryGetValue(objectType, out var creator))
            {
                return creator(message.MessageBytes);
            }
            throw new ArgumentException("Unrecognized object type.");
        }

    }
}
