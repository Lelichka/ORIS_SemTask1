using System.Net;
using System.Text;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class DanceStyleResult : Result
{
    private static readonly Template _template;

    static DanceStyleResult()
    {
        var data = File.ReadAllText("Views/dancestyle.sbnhtml");
        _template = Template.Parse(data);
    }
    public DanceStyleResult( DanceStyle style, List<Coach> coaches)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new { style = style, coaches = coaches }));
    }
}