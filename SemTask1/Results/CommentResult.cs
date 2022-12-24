using System.Net;

namespace SemTask1.Results;

public class CommentResult : Result 
{

    public CommentResult()
    {
        StatusCode = HttpStatusCode.Redirect;
        RedirectPath = "/";
    }
}