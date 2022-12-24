using System;
using System.Net;
using System.IO;
using System.Reflection.Metadata;

namespace SemTask1;
class Program
{
    private static bool isRun = true;
    static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(@"D:\ITIS\2022-2023\Inf\semtask1_rep\ORIS_SemTask1\SemTask1");
        var server = new HttpServer();
        while (isRun)
        {
            UserCommand(Console.ReadLine()?.ToLower(),server);
        }
    }

    public static void UserCommand(string command, HttpServer server)
    {
        switch (command)
        {
            case "start":
                server.StartServer();
                break;
            case "restart":
                server.StopServer();
                server.StartServer();
                break;
            case "stop":
                server.StopServer();
                break;
            case "close":
                isRun = false;
                break;
            default:
                Console.WriteLine("Неизвестная команда");
                break;
        }
    }
}