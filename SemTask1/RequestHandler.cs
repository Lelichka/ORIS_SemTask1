using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using HttpServer.session;
using SemTask1.Attributes;
using SemTask1.Models;
using SemTask1.Results;
using SemTask1.Services;

namespace SemTask1;

public static class RequestHandler
{
     public static async Task MakeResponseAsync(HttpListenerContext context, ServerSettings settings)
     {
          var response = context.Response;
          var request = context.Request;
          var p =  request.RawUrl.Split('/').TakeLast(2);
          var path = settings.Path + "/" + string.Join('/',p);
          if ( File.Exists(path))
          {
               FileHandler(response,path);
               return;
          }
          MethodHandler(context,settings);
     }
     //Рвзберись с сессиями

     private static Type? GetController(HttpListenerContext context, string pathController)
     {
         var request = context.Request;
         
         var assembly = Assembly.GetExecutingAssembly();
         var controller = assembly.GetTypes()
             .Where(t => Attribute.IsDefined(t, typeof(ApiController)))
             .FirstOrDefault(c => 
                 (((ApiController)c.GetCustomAttribute(typeof(ApiController))).ControllerName.ToLower() == pathController.ToLower() ||
                  c.Name.ToLower() == pathController.ToLower()));

         return controller;
     }

     private static MethodInfo? GetMethod(HttpListenerContext context,Type controller,string pathMethod)
     {
         var name = pathMethod.Split('/').Skip(1).Take(1).FirstOrDefault();
         var methodName = (name is null)?"":name;
         var request = context.Request; 
         return controller.GetMethods().FirstOrDefault(method =>
             method.GetCustomAttributes(true).Any(attr => attr.GetType().Name.ToLower() == $"http{request.HttpMethod.ToLower()}"
                        && ((HttpCustomMethod)attr).MethodURI == methodName));
     }

     private static void FileHandler(HttpListenerResponse response,string path)
     {
          byte[] buffer = null;

          if (Directory.Exists(path))
          {
               response.Headers.Set("Content-Type","text/html");
               buffer = File.ReadAllBytes(path  + @"html/index.html");
          }
          else if ( File.Exists(path))
          {
               response.Headers.Set("Content-Type",ContentTypeGetter.GetContentType(path));
               buffer = File.ReadAllBytes(path);
          }
          
          Stream output = response.OutputStream;
          output.Write(buffer, 0, buffer.Length);
          output.Close();
     }
     
    public static void MethodHandler(HttpListenerContext context, ServerSettings settings)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        string pathController;
        string pathMethod;
        
        if (request.RawUrl.Length == 1)
        {
            pathController = request.RawUrl;
            pathMethod = "";
        }
        else
        {
            pathController = "/" + string.Join("", request.RawUrl.Skip(1).TakeWhile(c => c != '/'));
            pathMethod = request.RawUrl.Replace(pathController, "");
        }
        
        var controller = GetController(context,pathController);
        if (controller is null) LoadErrorPage(response);
        
        var method = GetMethod(context, controller, pathMethod);
        if (method is null) LoadErrorPage(response);

        var methodArgs = GetMethodArgs(request,method,pathMethod);
        var result = (Result)(method.Invoke(Activator.CreateInstance(controller), methodArgs) as dynamic);
        
        response.ContentType = result.ContentType;
        response.StatusCode = (int)result.StatusCode;

        if (result.Cookies is not null)
            response.Cookies.Add(result.Cookies); 
        
        
        if (response.StatusCode == (int)HttpStatusCode.Redirect)
            response.Redirect(@$"http://localhost:{settings.Port}{result.RedirectPath}");

        using var output = response.OutputStream;
        output.Write(result.Buffer, 0, result.Buffer.Length);
    }
    
    
    private static List<string> GetRequestBody(HttpListenerRequest request)
    {
        using (Stream body = request.InputStream)
        {
            using (var reader = new StreamReader(body, request.ContentEncoding))
             return reader.ReadToEnd()
                 .Split('&')
                 .Select(pair => pair.Split('='))
                 .Select(pair => HttpUtility.UrlDecode(pair[1]))
                 .ToList();
        }
    }
    private static object[] GetMethodArgs(HttpListenerRequest request, MethodInfo method, string pathMethod)
    {
        var parameters = method.GetParameters();
        var args = pathMethod.Split('/').Skip(2).ToList();

        var query = new List<string>();

        if (request.HasEntityBody)
        { 
            query.AddRange(GetRequestBody(request));
        }

        if (method.Name == "PostLoginData" && query.Count == 2)
        {
            args.Add(string.Empty);
        }
        
        if (Attribute.IsDefined(method, typeof(AuthCookieRequired)))
        {
            var manager = SessionManager.Instance;
            var sessionId = CookieManager.GetAuthenticatedCookie(request.Cookies);
            if (sessionId != "")
                args.Add(manager.GetAccountIdFromSession(sessionId).ToString());
            else args.Add("0");
        }

        args = query.Concat(args).ToList();

        return parameters
         .Select((p, i) => Convert.ChangeType(args[i], p.ParameterType))
         .ToArray();
    }
    private static async Task LoadErrorPage(HttpListenerResponse response)
    {
        response.ContentType = "text/html";
        response.StatusCode = (int)HttpStatusCode.NotFound;

        var buffer = await File.ReadAllBytesAsync("static/html/ErrorPage.html");
        using var output = response.OutputStream;

        output.Write(buffer, 0, buffer.Length);
    }
}