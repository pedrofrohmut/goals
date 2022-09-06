using System.Data;
using Dapper;
using GoalsApi.Dtos;

namespace GoalsApi.DataAccess.Dapper;

public class DapperGoalDataAccess : GoalDataAccess 
{
    private readonly IDbConnection connection;

    public DapperGoalDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }
   
    public void AddGoal(CreateGoalDto newGoal, Guid userId) 
    {
        var sql = "INSERT INTO goals (text, user_id) VALUES (@text, @userId)";
        var text = newGoal.Text;
        connection.Query(sql, new { text, userId });
    }

    public IEnumerable<GoalDbDto> GetGoalsByUserId(Guid userId) 
    {
        var sql = "SELECT * FROM goals WHERE user_id = @userId";
        var rows = connection.Query(sql, new { userId });
        var goals = MapGoalRowsToDtos(rows);
        return goals;
    }

    private IEnumerable<GoalDbDto> MapGoalRowsToDtos(IEnumerable<dynamic> rows) =>
        rows.Select<dynamic, GoalDbDto>(row => new GoalDbDto() {
            Id = row.id,
            Text = row.text,
            UserId = row.user_id
        });
}
