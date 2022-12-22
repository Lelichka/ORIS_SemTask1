namespace SemTask1.Models;

public class Coach
{
    [NotInsertable]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string AboutCoach { get; set; }
    public string PicturePath { get; set; }
}