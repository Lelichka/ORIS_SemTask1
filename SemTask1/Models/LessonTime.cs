namespace SemTask1.Models;

public class LessonTime
{
    public int TimeId { get; set; }
    public int TimeNumb { get; set; }
    public TimeSpan LessStart { get; set; }
    public TimeSpan LessEnd { get; set; }

}
