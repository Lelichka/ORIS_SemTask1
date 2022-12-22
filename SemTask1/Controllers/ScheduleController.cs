using SemTask1.Attributes;
using SemTask1.Models;
using SemTask1.MyORM;
using SemTask1.Results;

namespace SemTask1.Controller;
[ApiController("/schedule")]
public class ScheduleController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    [HttpGet("")]
    public ScheduleResult GetSchedulePage()
    {
        var times = new ScheduleDAO(strConnection).GetLessonTime().ToList();
        var days = new ScheduleDAO(strConnection).GetWeekDays().Count();
        var lessons = GetDictionary(times.Count,days);
        
        return new ScheduleResult(lessons,times,days);
    }

    private Lesson[][] GetDictionary(int times,int days)
    {
        var schedule = new ScheduleDAO(strConnection).FindAll().ToList();

        var lessons = new Lesson[times][];
        for (int i = 0; i < times; i++)
        {
            lessons[i] = new Lesson[days];
        }
        foreach (var lesson in schedule)
        {
            var style = new DanceStylesDAO(strConnection).GetById(lesson.StyleId);
            var coach = new CoacesDAO(strConnection).GetById(lesson.CoachId);
            lessons[lesson.LTimeId][lesson.DayId] = new Lesson(style,coach);
        }

        return lessons;
    }
}