using System.Net;
using System.Text;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class AccountResult : Result
{
    private static readonly Template _template;

    static AccountResult()
    {
        var data = File.ReadAllText("Views/account.sbnhtml");
        _template = Template.Parse(data);
    }
    public AccountResult(User user)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {user = user}));
    }
}