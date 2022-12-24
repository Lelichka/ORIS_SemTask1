using SemTask1.Attributes;
using SemTask1.Models;
using SemTask1.MyORM;
using SemTask1.Results;

namespace SemTask1.Controller;
[ApiController("/comment")]
public class CommentController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    [HttpPost("")]
    [AuthCookieRequired]
    public CommentResult AddComment(string message,int id)
    {
        var userName = new UsersDAO(strConnection).GetById(id).UserName;
        var comment = new Comment(){UserName = userName,UserId = id, Message = message};
        new CommentsDAO(strConnection).Create(comment);
        return new CommentResult();
    }
    
    [HttpGet("delete")]
    public CommentResult DeleteComment(int id)
    {
        new CommentsDAO(strConnection).DeleteById(id);
        return new CommentResult();
    }
    [HttpPost("update")]
    public CommentResult Update( string message,int id)
    {
        new CommentsDAO(strConnection).UpdateMessageById(id,message);
        return new CommentResult();
    }
    
}