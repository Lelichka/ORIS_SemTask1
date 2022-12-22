using SemTask1;

namespace HttpServer.Models;

public class Session
{
    public Session() { }

    public Session(Guid id, int userId, string login, string password)
    {
        Id = id;
        UserId = userId;
        Login = login;
        Password = password;
    }

    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}