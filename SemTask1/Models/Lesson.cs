namespace SemTask1.Models;

public class Lesson
{
    public DanceStyle DanceStyle { get; set; }
    public Coach Coach { get; set; }

    public Lesson(DanceStyle style, Coach coach)
    {
        DanceStyle = style;
        Coach = coach;
    }
}