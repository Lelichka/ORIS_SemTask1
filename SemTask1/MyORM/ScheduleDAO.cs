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
    public WeekDay? GetWeekDayById(int id)
    {
        return new Database(StrConnection).GetById<WeekDay>(id, "WeekDay");
    }
    public IEnumerable<LessonTime> GetLessonTime()
    {
        return new Database(StrConnection).Select<LessonTime>( "LessonTime");
    }
    public LessonTime? GetLessonTimeById(int id)
    {
        return new Database(StrConnection).GetById<LessonTime>(id, "LessonTime");
    }

    public IEnumerable<ScheduleDB> FindAll()
    {
        return new Database(StrConnection).Select<ScheduleDB>( "Schedule");
    }

    public void Create(ScheduleDB entity)
    {
        new Database(StrConnection).Insert(entity, "Schedule");
    }

    public void Update(ScheduleDB entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(ScheduleDB entity)
    {
        new Database(StrConnection).Delete(entity,"Schedule");
    }
}