using GoalsApi.DataAccess.Dapper;
using GoalsApi.Dtos;
using GoalsApi.UseCases.Goals;
using GoalsApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IConfiguration configuration;

    public GoalsController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    // @Desc Add a Goal
    // @Route POST api/goals
    // @Access private
    [HttpPost]
    public ActionResult AddGoal([FromHeader] string authorization, [FromBody] CreateGoalDto newGoal)
    {
        var userId = TokenManager.GetUserIdFromToken(configuration, authorization);
        var connection = ConnectionManager.GetConnectionFromConfig(configuration);
        var userDataAccess = new DapperUserDataAccess(connection);
        var goalDataAccess = new DapperGoalDataAccess(connection);
        var addGoalUseCase = new AddGoalUseCase(userDataAccess, goalDataAccess);
        try {
            ConnectionManager.OpenConnection(connection);
            addGoalUseCase.Execute(newGoal, userId);
            return new ObjectResult("Goal Criado") { StatusCode = 201 };
        } catch (ArgumentException e) {
            return new ObjectResult(e.Message) { StatusCode = 400 };
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { StatusCode = 500 };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }

    // @Desc Get all goals from user
    // @Route GET api/goals/user/123
    // @Access private
    [HttpGet]
    public ActionResult GetGoals([FromHeader] string authorization)
    {
        var userId = TokenManager.GetUserIdFromToken(configuration, authorization);
        var connection = ConnectionManager.GetConnectionFromConfig(configuration);
        var userDataAccess = new DapperUserDataAccess(connection);
        var goalDataAccess = new DapperGoalDataAccess(connection);
        var getGoalsUseCase = new GetGoalsUseCase(userDataAccess, goalDataAccess);
        try {
            ConnectionManager.OpenConnection(connection);
            var goals = getGoalsUseCase.Execute(userId);
            return new ObjectResult(goals) { StatusCode = 200 };
        } catch (ArgumentException e) {
            return new ObjectResult(e.Message) { StatusCode = 400 };
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { StatusCode = 500 };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }

    // @Desc Get all goals
    // @Route POST api/goals
    // @Access private
    [HttpPut("{id}")]
    public ActionResult UpdateGoal(Guid id)
    {
        return Ok("Not implemented");
    }

    // @Desc Get all goals
    // @Route POST api/goals
    // @Access private
    [HttpDelete("{id}")]
    public ActionResult DeleteGoal([FromHeader] string authorization, Guid id)
    {
        var userId = TokenManager.GetUserIdFromToken(configuration, authorization);
        var connection = ConnectionManager.GetConnectionFromConfig(configuration);
        var userDataAccess = new DapperUserDataAccess(connection);
        var goalDataAccess = new DapperGoalDataAccess(connection);
        var deleteGoalUseCase = new DeleteGoalUseCase(userDataAccess, goalDataAccess);
        try {
            ConnectionManager.OpenConnection(connection);
            deleteGoalUseCase.Execute(id, userId);
            return new ObjectResult("") { StatusCode = 204 };
        } catch (ArgumentException e) {
            return new ObjectResult(e.Message) { StatusCode = 400 };
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { StatusCode = 500 };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }
}
