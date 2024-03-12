﻿using OOD_24L_01180686.source.ServerActions;
using OOD_24L_01180686.source.Readers;
using System.Text;
using OOD_24L_01180686.source.Writers;


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
                        Console.WriteLine("Objects count: " + Server.Objects.Count());
                        JSONWriter writer = new JSONWriter();
                        writer.WriteData(Server.Objects, Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\DataFiles\\snapshot_{DateTime.Now:HH_mm_ss}.json");
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
