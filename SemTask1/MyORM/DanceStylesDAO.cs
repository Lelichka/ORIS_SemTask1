
using SemTask1.Models;

namespace SemTask1.MyORM;

public class DanceStylesDAO
{
    private string StrConnection { get; set; }

    public DanceStylesDAO(string strConnection)
    {
        StrConnection = strConnection;
    }
    public DanceStyle GetById(int id)
    {
        return new Database(StrConnection).GetById<DanceStyle>(id, "DanceStyle");
    }
    public List<DanceStyle> GetByCoachId(int id)
    {
        var pairs = new Database(StrConnection).ExecuteQuery<StyleCoachPair>($"select {string.Join(',',typeof(StyleCoachPair).GetProperties().Select(p => p.Name).ToList())} from StyleCoach").ToList();
        pairs = pairs.Where(pair => pair.CoachId == id).ToList();
        return new Database(StrConnection).ExecuteQuery<DanceStyle>(
                $"select {string.Join(',', typeof(DanceStyle).GetProperties().Select(p => p.Name).ToList())} from DanceStyle where Id in ({string.Join(',', pairs.Select(pair => pair.StyleId.ToString()))})")
            .ToList();
    }

    public IEnumerable<DanceStyle> FindAll()
    {
        return new Database(StrConnection).Select<DanceStyle>( "DanceStyle");
    }

    public void Create(DanceStyle entity)
    {
        new Database(StrConnection).Insert(entity, "DanceStyle");
    }

    public void Update(DanceStyle entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(DanceStyle entity)
    {
        new Database(StrConnection).Delete(entity,"DanceStyle");
    }
}