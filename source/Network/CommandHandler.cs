using OOD_24L_01180686.source.Writers;
using OOD_24L_01180686.source.Objects;


namespace OOD_24L_01180686.source.Network
{
    public class CommandHandlerClass
    {
        public static void CommandHandler(Server server)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "start")
                {
                    if (!Server.IsRunning)
                    {
                        server.StartServer();
                    }
                    else
                    {
                        Console.WriteLine("Server is already running.");
                    }
                }
                else if (input.ToLower() == "print")
                {
                    if (Server.IsRunning)
                    {
                        Console.WriteLine("Creating a snapshot...");
                        foreach(var obj in Server.Objects)
                        {
                            if(obj is Flight flight)
                            {
                                flight.UpdatePosition();
                            }
                        }
                        Console.WriteLine("Objects count: " + Server.Objects.Count());
                        JSONWriter writer = new JSONWriter();
                        writer.WriteData(Server.Objects,
                            Directory.GetCurrentDirectory() +
                            $"..\\..\\..\\..\\DataFiles\\snapshot_{DateTime.Now:HH_mm_ss}.json");
                    }
                    else
                    {
                        Console.WriteLine("Server is not running.");
                    }
                }
                else if (input.ToLower() == "exit")
                {
                    if (Server.IsRunning)
                    {
                        server.StopServer().Wait();
                        Console.WriteLine("Server stopped.");
                    }

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