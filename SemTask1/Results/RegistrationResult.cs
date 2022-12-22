using System.Net;
using System.Text;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class RegistrationResult : Result
{
    private static readonly Template _template;

    static RegistrationResult()
    {
        var data = File.ReadAllText("Views/registration.sbnhtml");
        _template = Template.Parse(data);
    }
    
    public RegistrationResult(bool isRegistrationSuccess, bool isPasswordSuccess)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {isRegistrationSuccess = isRegistrationSuccess, isPasswordSuccess = isPasswordSuccess }));
    }
    public RegistrationResult()
    {
        StatusCode = HttpStatusCode.Redirect;
        RedirectPath = "/login";
        Buffer = new byte[] { };
    }
}