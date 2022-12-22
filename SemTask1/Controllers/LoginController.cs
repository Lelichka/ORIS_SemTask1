using System.Text;
using SemTask1.Attributes;
using SemTask1.MyORM;
using SemTask1.Results;
using SemTask1.Services;

namespace SemTask1.Controller;

[ApiController("/login")]
public class LoginController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    
    [HttpGet("")]
    public LoginResult GetLoginPage()
    {
        return new LoginResult(false,false);
    }
    
    [HttpPost("")]
    public LoginResult PostLoginData(string login, string password, string rememberMe)
    {
        var user =  new UsersDAO(strConnection).GetByEmail(login);
        if (user is null) user = new UsersDAO(strConnection).GetByUserName(login);

        if (user is null)
            return new LoginResult(true, false);

        if (ExpressionEncoder.Encrypt(password + user!.Guid) == user.EncyptedPassword)
            return new LoginResult(user!,rememberMe, login );

        return new LoginResult(false, true);
    }
    [HttpGet("LogOff")]
    public LoginResult LogOffAndGetMainPage()
    {
        return new LoginResult();
    }
    
}