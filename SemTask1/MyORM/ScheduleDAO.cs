using SemTask1.Models;

namespace SemTask1.MyORM;

public class ScheduleDAO
{
    private string StrConnection { get; set; }
    public ScheduleDAO(string strConnection)
    {
        StrConnection = strConnection;
    }

    public IEnumerable<WeekDay> GetWeekDays()
    {
        return new Database(StrConnection).Select<WeekDay>( "WeekDay");
    }
    public IEnumerable<LessonTime> GetLessonTime()
    {
        return new Database(StrConnection).Select<LessonTime>( "LessonTime");
    }

    public IEnumerable<ScheduleDB> FindAll()
    {
        return new Database(StrConnection).Select<ScheduleDB>( "Schedule");
    }
    
}