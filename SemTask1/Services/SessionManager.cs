using HttpServer.Models;
using SemTask1.Models;
using SemTask1.MyORM;

namespace HttpServer.session;
using Microsoft.Extensions.Caching.Memory;

public class SessionManager
{
    private SessionManager()
    {
    }
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    private static readonly Lazy<SessionManager> Lazy =
        new(() => new SessionManager());
    public static SessionManager Instance => Lazy.Value;
    

    public void CreateSession(Guid guid, User user, string login)
    {
        new SessionsDAO(strConnection).Create(new Session(guid,user.Id,login,user.EncyptedPassword));
    }
    public int GetAccountIdFromSession(string id)
    {
        var session = new SessionsDAO(strConnection).GetById(id);

        return session is not null ? session.UserId : 0;
    }
    public Guid GetSessionIdByUserId(int id)
    {
        var session = new SessionsDAO(strConnection).GetByUserId(id);

        return session.Id;
    }

    public void DeleteSession(string id)
    {
        new SessionsDAO(strConnection).DeleteById(id);
    }


    
    
}