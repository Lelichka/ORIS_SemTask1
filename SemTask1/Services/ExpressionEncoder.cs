using System.Security.Cryptography;
using System.Text;

namespace SemTask1.Services;

public static class ExpressionEncoder
{
    public static string CreateGuid() => Guid.NewGuid().ToString();

    public static string Encrypt(string password)
    {
        var inputBytes = Encoding.UTF8.GetBytes(password);
        
        using var sha256 = SHA256.Create();

        var outputBytes = sha256.ComputeHash(inputBytes);

        return string.Join("", outputBytes.Select(b => b.ToString()));
    }
}