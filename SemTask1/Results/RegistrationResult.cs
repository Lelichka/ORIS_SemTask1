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
    
    public RegistrationResult(bool isAllFieldsFill,bool isRegistrationSuccess, bool isPasswordSuccess, bool isEmailCorrect, bool isUserCorrect)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {isAllFieldsFill = isAllFieldsFill,isRegistrationSuccess = isRegistrationSuccess, isPasswordSuccess = isPasswordSuccess,isEmailCorrect = isEmailCorrect, isUserCorrect = isUserCorrect  }));
    }
    public RegistrationResult()
    {
        StatusCode = HttpStatusCode.Redirect;
        RedirectPath = "/login";
        Buffer = new byte[] { };
    }
}