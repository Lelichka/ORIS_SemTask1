using System.Net;
using System.Text;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class CoachesResult : Result
{
    private static readonly Template _template;

    static CoachesResult()
    {
        var data = File.ReadAllText("Views/coaches.sbnhtml");
        _template = Template.Parse(data);
    }
    public CoachesResult(List<Coach> coaches)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {coaches = coaches, count = coaches.Count - 1 }));
    }

}