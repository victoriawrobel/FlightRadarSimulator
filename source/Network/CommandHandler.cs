using OOD_24L_01180686.source.Writers;
using OOD_24L_01180686.source.Objects;
using OOD_24L_01180686.source.ObjectsCollection;


namespace OOD_24L_01180686.source.Network
{
    public class CommandHandlerClass
    {
        public static void CommandHandler(Server server)
        {
            server.StartServer();
            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "print")
                {
                    Console.WriteLine("Creating a snapshot...");
                    foreach (var obj in ObjectsCollection.ObjectsCollection.GetObjects())
                    {
                        if (obj is Flight flight)
                        {
                            flight.UpdatePosition();
                        }
                    }

                    Console.WriteLine("Objects count: " + ObjectsCollection.ObjectsCollection.GetObjects().Count());
                    JSONWriter writer = new JSONWriter();
                    writer.WriteData(ObjectsCollection.ObjectsCollection.GetObjects(),
                        Directory.GetCurrentDirectory() +
                        $"..\\..\\..\\..\\DataFiles\\snapshot_{DateTime.Now:HH_mm_ss}.json");
                }
                else if (input.ToLower() == "exit")
                {
                    server.StopServer().Wait();
                    Console.WriteLine("Server stopped.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command. Available commands: start, print, exit");
                }
            }
        }
    }
}