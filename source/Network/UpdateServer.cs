using NetworkSourceSimulator;
using OOD_24L_01180686.source.Objects;
using OOD_24L_01180686.source.Updates;
using OOD_24L_01180686.source.Reports;
using OOD_24L_01180686.source.Visualization;

namespace OOD_24L_01180686.source.Network
{
    public class UpdateServer
    {
        private NetworkSourceSimulator.NetworkSourceSimulator updateServer;
        private List<IObserver> observers = new List<IObserver>();
        private int MinDelay = 400;
        private int MaxDelay = 400;
        private static UpdateServer serverInstance;
        private static object serverLock = new object();

        public static UpdateServer GetInstance(string filepath)
        {
            lock (serverLock)
            {
                if (serverInstance == null)
                {
                    serverInstance = new UpdateServer(filepath);
                }
            }

            return serverInstance;
        }

        private UpdateServer(string filepath)
        {
            Thread.Sleep(4000);
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"File {filepath} not found");
            }

            updateServer = new NetworkSourceSimulator.NetworkSourceSimulator(filepath, MinDelay, MaxDelay);
            updateServer.OnIDUpdate += OnIDUpdateHandler;
            updateServer.OnPositionUpdate += OnPositionUpdateHandler;
            updateServer.OnContactInfoUpdate += OnContactInfoUpdateHandler;

            RegisterObserver(new Logger());
            RegisterObserver(new EntitySearch());
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        private void NotifyIDUpdateObservers(IDUpdateArgs args)
        {
            foreach (var observer in observers)
            {
                observer.Update(args);
            }
        }

        private void NotifyPositionUpdateObservers(PositionUpdateArgs args)
        {
            foreach (var observer in observers)
            {
                observer.Update(args);
            }
        }

        private void NotifyContactInfoUpdateObservers(ContactInfoUpdateArgs args)
        {
            foreach (var observer in observers)
            {
                observer.Update(args);
            }
        }

        private void OnIDUpdateHandler(object sender, IDUpdateArgs args)
        {
            NotifyIDUpdateObservers(args);
        }

        private void OnPositionUpdateHandler(object sender, PositionUpdateArgs args)
        {
            NotifyPositionUpdateObservers(args);
        }

        private void OnContactInfoUpdateHandler(object sender, ContactInfoUpdateArgs args)
        {
            NotifyContactInfoUpdateObservers(args);
        }


        public void StartServer()
        {
            Console.WriteLine("Update server started.");
            Task.Run(() => updateServer.Run());
        }

        public Task StopServer()
        {
            Console.WriteLine("Update server stopping...");
            return Task.CompletedTask;
        }
    }
}