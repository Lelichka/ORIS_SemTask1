namespace SemTask1.Models;

public class Comment
{
    [NotInsertable]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string Message { get; set; }
}