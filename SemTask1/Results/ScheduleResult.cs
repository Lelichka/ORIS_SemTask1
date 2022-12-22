using System.Net;
using System.Text;
using Scriban;
using SemTask1.Models;

namespace SemTask1.Results;

public class ScheduleResult : Result
{
    private static readonly Template _template;

    static ScheduleResult()
    {
        var data = File.ReadAllText("Views/schedule.sbnhtml");
        _template = Template.Parse(data);
    }
    public ScheduleResult(Lesson[][] lessons, List<LessonTime> lessonTimes, int dayCount)
    {
        StatusCode = HttpStatusCode.OK;
        ContentType = "text/html";
        Buffer = Encoding.UTF8.GetBytes(
            _template.Render(new {lessons = lessons,lessonTimes = lessonTimes, dayCount = dayCount-1}));
    }
}