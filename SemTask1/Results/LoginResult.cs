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
    public LoginResult(int id)
    {
        StatusCode = HttpStatusCode.Redirect;
        RedirectPath = "/";
        var sessionId = SessionManager.Instance.GetSessionIdByUserId(id);
        SessionManager.Instance.DeleteSession(sessionId.ToString());
        var cookie = new Cookie("SessionId",sessionId.ToString())
        {
            Expires = DateTime.Now.AddDays(-1d)
        };
        Cookies = new CookieCollection() { cookie };
        
    }
}