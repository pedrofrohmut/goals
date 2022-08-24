using GoalsDomain.Dtos;
using GoalsDomain.UseCases;

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
    public async Task<ActionResult> AddGoal() {
        var msg = await this.goalsUseCases.AddGoal(new CreateGoalDto());
        return Ok(msg);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetGoals() {
        var msg = await this.goalsUseCases.GetGoals();
        return Ok(msg);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGoal(string id) {
        var msg = await this.goalsUseCases.UpdateGoal(id);
        return Ok(msg);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGoal(string id) {
        var msg = await this.goalsUseCases.DeleteGoal(id);
        return Ok("Delete Goal " + id);
    }
}
