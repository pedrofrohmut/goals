using GoalsApi.Dtos;
using GoalsApi.UseCases;

using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly GoalsUseCases goalsUseCases;
   
    public GoalsController()
    {
        this.goalsUseCases = new GoalsUseCases();
    }
   
   
    [HttpPost]
    public ActionResult AddGoal() {
        var msg = goalsUseCases.AddGoal(new CreateGoalDto());
        return Ok(msg);
    }
    
    [HttpGet]
    public ActionResult GetGoals() {
        var msg = goalsUseCases.GetGoals();
        return Ok(msg);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateGoal(string id) {
        var msg = goalsUseCases.UpdateGoal(id);
        return Ok(msg);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteGoal(string id) {
        var msg = goalsUseCases.DeleteGoal(id);
        return Ok("Delete Goal " + id);
    }
}
