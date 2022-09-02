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
}
