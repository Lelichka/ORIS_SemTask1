using System.Net;
using System.Text;
using SemTask1.Models;

namespace SemTask1.Results;
using Scriban;

public class MainResult : Result
{
    private static readonly Template _template;

    static MainResult()
    {
        var data = File.ReadAllText("Views/main.sbnhtml");
        _template = Template.Parse(data);
    }
    public MainResult( List<DanceStyle> styles, List<Comment> comments, bool isAutorised)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new { styles = styles, comments = comments, isAutorised = isAutorised }));
    }
}
