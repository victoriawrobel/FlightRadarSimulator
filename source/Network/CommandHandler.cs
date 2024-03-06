using OOD_24L_01180686.source.ServerActions;


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
                        Task.Run(() => server.StartServer());
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
                        Server.CreateSnapshot();
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
