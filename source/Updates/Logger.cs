using NetworkSourceSimulator;
using OOD_24L_01180686.source;
using System.IO;

namespace OOD_24L_01180686.source.Updates
{
    public class Logger : IObserver
    {
        private string logFilePath;

        public Logger()
        {
            logFilePath = Directory.GetCurrentDirectory() +  $"..\\..\\..\\..\\DataFiles\\log_{DateTime.Now:yyyy-MM-dd}.txt";
        }

        public void Log(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now:HH:mm:ss} - {message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public void Update(IDUpdateArgs args)
        {
            lock (EntitySearch.lockObject)
            {
                if (!EntitySearch.EntitySearchDictionary.ContainsKey(args.ObjectID))
                {
                    Log($"ID Update - ObjectID: {args.ObjectID}. Original ID not found.");
                }
                else if (EntitySearch.EntitySearchDictionary.ContainsKey(args.NewObjectID))
                {
                    Log($"ID Update - ObjectID: {args.ObjectID}. New ID already in use.");
                }
                else
                {
                    Log($"ID Update - ObjectID: {args.ObjectID}, NewObjectID: {args.NewObjectID}. Success.");
                }
            }
        }

        public void Update(PositionUpdateArgs args)
        {
            lock (EntitySearch.lockObject)
            {
                if (EntitySearch.EntitySearchDictionary.ContainsKey(args.ObjectID))
                {
                    Log($"Position Update - ObjectID: {args.ObjectID}, Longitude: {args.Longitude}, Latitude: {args.Latitude}, AMSL: {args.AMSL}. Success.");
                }
                else
                {
                    Log($"Position Update - ObjectID: {args.ObjectID}. Object not found.");
                }
            }   
        }

        public void Update(ContactInfoUpdateArgs args)
        {
            lock (EntitySearch.lockObject)
            {
                if (EntitySearch.EntitySearchDictionary.ContainsKey(args.ObjectID))
                {
                    Log(
                        $"Contact Info Update - ObjectID: {args.ObjectID}, PhoneNumber: {args.PhoneNumber}, EmailAddress: {args.EmailAddress}. Success.");
                }
                else
                {
                    Log($"Contact Info Update - ObjectID: {args.ObjectID}. Object not found.");
                }
            }
        }
    }
}
