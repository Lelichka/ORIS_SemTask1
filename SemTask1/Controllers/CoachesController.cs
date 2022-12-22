using SemTask1.Attributes;
using SemTask1.MyORM;
using SemTask1.Results;

namespace SemTask1.Controller;

[ApiController("/coaches")]
public class CoachesController
{
    private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
    [HttpGet("getCoaches")]
    public CoachesResult GetCoachesPage()
    {
        var coaches = new CoacesDAO(strConnection).FindAll().ToList();
        return new CoachesResult(coaches);
    }
    [HttpGet("getCoach")]
    public CoachResult GetCoachPage(int coachId )
    {
        var coaches = new CoacesDAO(strConnection).GetById(coachId);
        var styles = new DanceStylesDAO(strConnection).GetByCoachId(coachId);
        return new CoachResult(coaches,styles);
    }
}