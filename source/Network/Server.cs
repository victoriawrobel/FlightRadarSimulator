using NetworkSourceSimulator;
using OOD_24L_01180686.source.Readers;
using System.Text;
using OOD_24L_01180686.source.Objects;
using OOD_24L_01180686.source.Reports;

namespace OOD_24L_01180686.source.Network
{
    public class Server
    {
        private NetworkSourceSimulator.NetworkSourceSimulator server;
        private static Server serverInstance;
        private static object serverLock = new object();
        private string Filepath;
        private int MaxDelay = 0;
        private int MinDelay = 0;

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

            Console.WriteLine("Server started.");
            server = new NetworkSourceSimulator.NetworkSourceSimulator(Filepath, MinDelay, MaxDelay);
            server.OnNewDataReady += ServerOnNewDataReady;
            Task.Run(() => server.Run());
        }

        public Task StopServer()
        {
            Console.WriteLine("Server stopping...");
            return Task.CompletedTask;
        }

        private void ServerOnNewDataReady(object sender, NewDataReadyArgs e)
        {
            var message = server.GetMessageAt(e.MessageIndex);
            var obj = MessageParser(message);
            EntitySearch.AddObject(obj);
            if ((Entity)obj != null)
            {
                EntitySearch.AddObject((Entity)obj);
            }

            if (obj as Flight != null)
            {
                EntitySearch.AddFlight(obj as Flight);
            }

            if (obj as IReportable != null)
            {
                EntitySearch.AddReportable(obj as IReportable);
            }
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