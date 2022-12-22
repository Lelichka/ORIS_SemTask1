using SemTask1.Models;

namespace SemTask1.MyORM;

public class CommentsDAO
{
    private string StrConnection { get; set; }

    public CommentsDAO(string strConnection)
    {
        StrConnection = strConnection;
    }
    public Comment GetById(int id)
    {
        return new Database(StrConnection).GetById<Comment>(id, "Сomments");
    }

    public IEnumerable<Comment> FindAll()
    {
        return new Database(StrConnection).Select<Comment>( "Comments");
    }

    public void Create(Comment entity)
    {
        new Database(StrConnection).Insert(entity, "Comments");
    }

    public void Update(Comment entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Comment entity)
    {
        new Database(StrConnection).Delete(entity,"Comments");
    }
}