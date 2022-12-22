using SemTask1.Attributes;
using SemTask1.MyORM;
using SemTask1.Results;

namespace SemTask1.Controller;

[ApiController("/myPage")]
public class UserAcoountController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    [HttpGet("")]
    [AuthCookieRequired]
    public AccountResult GetUserPage(int id)
    {
        var user = new UsersDAO(strConnection).GetById(id);
        return new AccountResult(user);
    }
}