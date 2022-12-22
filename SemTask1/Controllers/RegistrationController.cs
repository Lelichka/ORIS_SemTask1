using SemTask1.Attributes;
using SemTask1.Models;
using SemTask1.MyORM;
using SemTask1.Results;
using SemTask1.Services;

namespace SemTask1.Controller;

[ApiController("/registration")]
public class RegistrationController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    
    [HttpGet("")]
    public RegistrationResult GetRegistrationPage()
    {
        return new RegistrationResult(true,true);
    }
    [HttpPost("")]
    public RegistrationResult PostRegistrationData(string userName, string email, string password, string passwordRepet)
    {
        var userDAO = new UsersDAO(strConnection);
        var isRegistrationSuccess = userDAO.GetByEmail(email) is null && userDAO.GetByUserName(userName) is null;
        var isPasswordSuccess = password == passwordRepet;
        if (!isRegistrationSuccess || !isPasswordSuccess)
            return new RegistrationResult(isRegistrationSuccess,isPasswordSuccess);
        
        var guid = ExpressionEncoder.CreateGuid();
        var encyptedPassword = ExpressionEncoder.Encrypt(password + guid);
        
        userDAO.Create(new User(userName,email,encyptedPassword,guid));
        return new RegistrationResult();
    }
    
}