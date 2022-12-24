
using SemTask1.Models;

namespace SemTask1.MyORM;

public class CoacesDAO
{
    private string StrConnection { get; set; }

    public CoacesDAO(string strConnection)
    {
        StrConnection = strConnection;
    }

    public List<Coach> GetByStyleId(int id)
    {
        var pairs = new Database(StrConnection).ExecuteQuery<StyleCoachPair>($"select {string.Join(',',typeof(StyleCoachPair).GetProperties().Select(p => p.Name).ToList())} from StyleCoach").ToList();
        pairs = pairs.Where(pair => pair.StyleId == id).ToList();
        return new Database(StrConnection).ExecuteQuery<Coach>(
                $"select {string.Join(',', typeof(Coach).GetProperties().Select(p => p.Name).ToList())} from Coaches where Id in ({string.Join(',', pairs.Select(pair => pair.CoachId.ToString()))})")
            .ToList();
    }
    
    public Coach GetById(int id)
    {
        return new Database(StrConnection).GetById<Coach>(id, "Coaches");
    }

    public IEnumerable<Coach> FindAll()
    {
        return new Database(StrConnection).Select<Coach>( "Coaches");
    }
    
}