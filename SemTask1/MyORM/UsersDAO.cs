
using SemTask1.Models;

namespace SemTask1.MyORM;

public class UsersDAO 
{
    private string StrConnection { get; set; }

    public UsersDAO(string strConnection)
    {
        StrConnection = strConnection;
    }
    public User? GetById(int id)
    {
        return new Database(StrConnection).GetById<User>(id, "Users");
    }
    public User? GetByEmail(string email)
    {
        return new Database(StrConnection).ExecuteQuery<User>(
            $"select {string.Join(',', typeof(User).GetProperties().Select(p => p.Name).ToList())} from Users where Email = '{email}'").FirstOrDefault();
    }
    public User? GetByUserName(string userName)
    {
        return new Database(StrConnection).ExecuteQuery<User>(
            $"select {string.Join(',', typeof(User).GetProperties().Select(p => p.Name).ToList())} from Users where UserName = '{userName}'").FirstOrDefault();
    }

    public IEnumerable<User> FindAll()
    {
        return new Database(StrConnection).Select<User>( "Users");
    }

    public void Create(User entity)
    {
        new Database(StrConnection).Insert(entity, "Users");
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(User entity)
    {
        new Database(StrConnection).Delete(entity,"Users");
    }
}