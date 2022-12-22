using System.Net;
using System.Text;
using HttpServer.session;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class LoginResult : Result
{
    private static readonly Template _template;

    static LoginResult()
    {
        var data = File.ReadAllText("Views/login.sbnhtml");
        _template = Template.Parse(data);
    }
    public LoginResult(bool isLoginNotExist, bool isWrongPassword)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {isLoginNotExist = isLoginNotExist, isWrongPassword = isWrongPassword }));
    }
    public LoginResult(User user, string rememberMe, string login)
    {
        StatusCode = HttpStatusCode.Redirect;
        RedirectPath = "/";

        var sessionId = Guid.NewGuid();

        if (rememberMe != "")
        {
            var cookie = new Cookie("SessionId", sessionId.ToString());
            cookie.Expires = DateTime.Now.AddYears(1);
            Cookies = new CookieCollection() { cookie };
        }
        else
        {
            var cookie = new Cookie("SessionId", sessionId.ToString());
            cookie.Expires = DateTime.Now.AddHours(1);
            Cookies = new CookieCollection() { cookie };
        }

        var manager = SessionManager.Instance;
        manager.CreateSession(sessionId, user, login);
    }
    public LoginResult()
    {
        StatusCode = HttpStatusCode.Redirect;
        RedirectPath = "/";

        
        Cookies = new CookieCollection() {  };
        
    }
}