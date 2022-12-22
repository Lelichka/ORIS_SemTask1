namespace SemTask1.Models;

public class DanceStyle
{
    [NotInsertable]
    public int Id { get; set; }
    public string StyleName { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public double LessonCost { get; set; }
    public string PicturePath { get; set; }
}