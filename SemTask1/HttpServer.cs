using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Data.SqlClient;
using SemTask1.Models;

namespace SemTask1;
public class HttpServer
{
    private HttpListener listener;
    private ServerSettings? settings;
    
    public HttpServer()
    {
        listener = new HttpListener();
    }

    public void StartServer()
    {
        if (listener.IsListening)
        {
            Console.WriteLine("Сервер уже запущен");
            return;
        }
        settings = JsonSerializer.Deserialize<ServerSettings>(File.ReadAllBytes(@"D:\ITIS\2022-2023\Inf\SemTask1\SemTask1\settings.json"));
        
        listener.Prefixes.Clear();
        listener.Prefixes.Add($"http://localhost:{settings.Port}/");
        
        listener.Start();
        Console.WriteLine($"Сервер запущен http://localhost:{settings.Port}/");

        ListenAsync();
    }

    public void StopServer()
    {
        if (!listener.IsListening)
        {
            Console.WriteLine("Сервер не запущен");
            return;
        }
        listener.Stop();
        Console.WriteLine("Сервер остановлен");
    }

    private async Task ListenAsync()
    {
        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync();

            RequestHandler.MakeResponseAsync(context, settings);
                
        }
    }

}