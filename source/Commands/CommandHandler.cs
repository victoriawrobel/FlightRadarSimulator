using OOD_24L_01180686.source.Writers;
using OOD_24L_01180686.source.Objects;
using OOD_24L_01180686.source.Reports;
using OOD_24L_01180686.source.Network;

namespace OOD_24L_01180686.source.Commands
{
    public class CommandHandlerClass
    {
        public static void CommandHandler(Server server)
        {
            server.StartServer();

            NewsGenerator newsGenerator = new NewsGenerator(EntitySearch.GetReporters(), EntitySearch.GetReportables());
            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "print")
                {
                    Console.WriteLine("Creating a snapshot...");
                    foreach (var obj in EntitySearch.GetFlights())
                    {
                        if (obj != null)
                        {
                            obj.UpdatePosition();
                        }
                    }

                    Console.WriteLine("Objects count: " + EntitySearch.GetObjects().Count());
                    JSONWriter writer = new JSONWriter();
                    writer.WriteData(EntitySearch.GetObjects(),
                        Directory.GetCurrentDirectory() +
                        $"..\\..\\..\\..\\DataFiles\\snapshot_{DateTime.Now:HH_mm_ss}.json");
                }
                else if (input.ToLower() == "report")
                {
                    foreach (var news in newsGenerator)
                    {
                        Console.WriteLine(news);
                    }
                }
                else if (input.ToLower() == "exit")
                {
                    server.StopServer().Wait();
                    Console.WriteLine("Server stopped.");
                    break;
                }
                else
                {
                    Command command;
                    try { 
                        command = CommandParser.Parse(input);
                        command.Execute();
                        EntitySearch entitySearch = new EntitySearch();
                        Console.WriteLine("Command executed successfully.");
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}