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
            return new ObjectResult("Goal Criado") { StatusCode = StatusCodes.Status201Created };
        } catch (ArgumentException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { 
                StatusCode = StatusCodes.Status500InternalServerError 
            };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }

    // @Desc Get all goals from user
    // @Route GET api/goals/user/123
    // @Access private
    [HttpGet("user/123")]
    public ActionResult GetGoals(Guid userId)
    {
        return Ok("Hello get goals");
    }

    // @Desc Get all goals
    // @Route POST api/goals
    // @Access private
    [HttpPut("{id}")]
    public ActionResult UpdateGoal(Guid id)
    {
        return Ok();
    }

    // @Desc Get all goals
    // @Route POST api/goals
    // @Access private
    [HttpDelete("{id}")]
    public ActionResult DeleteGoal(Guid id)
    {
        return Ok();
    }
}
