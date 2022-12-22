using System.Net;
using System.Text;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class CoachResult : Result
{
    private static readonly Template _template;

    static CoachResult()
    {
        var data = File.ReadAllText("Views/coach.sbnhtml");
        _template = Template.Parse(data);
    }
    
    public CoachResult(Coach coach, List<DanceStyle> styles)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {coach = coach, styles = styles }));
    }
}