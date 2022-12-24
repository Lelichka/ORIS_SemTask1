using HttpServer.Models;

namespace SemTask1.MyORM;

public class SessionsDAO
{
    private string StrConnection { get; set; }

    public SessionsDAO(string strConnection)
    {
        StrConnection = strConnection;
    }
    public Session? GetById(string id)
    {
        return new Database(StrConnection).ExecuteQuery<Session>($"select {string.Join(',',typeof(Session).GetProperties().Select(p => p.Name).ToList())} from Sessions where Id = '{id}'").FirstOrDefault();
    }
    public Session? GetByUserId(int id)
    {
        return new Database(StrConnection).ExecuteQuery<Session>($"select {string.Join(',',typeof(Session).GetProperties().Select(p => p.Name).ToList())} from Sessions where UserId = {id}").FirstOrDefault();
    }

    public IEnumerable<Session> FindAll()
    {
        return new Database(StrConnection).Select<Session>( "Sessions");
    }

    public void Create(Session entity)
    {
        new Database(StrConnection).Insert(entity, "Sessions");
    }

    public void Update(Session entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Session entity)
    {
        new Database(StrConnection).Delete(entity,"Sessions");
    }
    public void DeleteById(string id)
    {
        new Database(StrConnection).ExecuteNonQuery($"delete from Sessions where Id = '{id}'");
    }
}