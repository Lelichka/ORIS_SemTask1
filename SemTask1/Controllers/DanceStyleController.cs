using SemTask1.Attributes;
using SemTask1.MyORM;
using SemTask1.Results;

namespace SemTask1.Controller;

[ApiController("/styles")]
public class DanceStyleController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    [HttpGet("getStyle")]
    public DanceStyleResult GetStylePage(int styleId)
    {
        var style = new DanceStylesDAO(strConnection).GetById(styleId);
        var coaches = new CoacesDAO(strConnection).GetByStyleId(styleId);
        return new DanceStyleResult(style,coaches);
    }
}