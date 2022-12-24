using System.Text.RegularExpressions;
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
        return new RegistrationResult(true,true, true,true,true);
    }
    [HttpPost("")]
    public RegistrationResult PostRegistrationData(string userName, string email, string password, string passwordRepet)
    {
        var isAllFieldFill = userName != "" && email != "" && password != "" && passwordRepet != "";
        if (!isAllFieldFill) return new RegistrationResult(false,true,true,true,true);
        var userDAO = new UsersDAO(strConnection);
        
        
        var regex1 = new Regex(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
        var isEmailCorrect = regex1.IsMatch(email);
        var regex2 = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
        var isUserCorrect = regex2.IsMatch(userName);
        
        if ( !isEmailCorrect || !isUserCorrect)
            return new RegistrationResult(true,true,true,isEmailCorrect,isUserCorrect);
        
        var isRegistrationSuccess = userDAO.GetByEmail(email) is null && userDAO.GetByUserName(userName) is null;
        if (!isRegistrationSuccess)
            return new RegistrationResult(true,isRegistrationSuccess,true,true,true);
        
        var isPasswordSuccess = password == passwordRepet;
        if (!isPasswordSuccess) 
            return new RegistrationResult(true,true,isPasswordSuccess,true,true);
        
        var guid = ExpressionEncoder.CreateGuid();
        var encyptedPassword = ExpressionEncoder.Encrypt(password + guid);
        
        
        
        userDAO.Create(new User(userName,email,encyptedPassword,guid));
        return new RegistrationResult();
    }
    
}