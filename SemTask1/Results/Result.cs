using System.Net;

namespace SemTask1.Results;

public abstract class Result
{
    public HttpStatusCode StatusCode { get; protected set; }
    public string? ContentType { get; protected set; }
    public byte[] Buffer { get; protected set; }
    public CookieCollection Cookies { get; protected set; }
    public string? RedirectPath { get; protected set; }
}