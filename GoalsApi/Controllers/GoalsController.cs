using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    [HttpPost]
    public ActionResult AddGoal() {
        return Ok("Add Goal");
    }
    
    [HttpGet]
    public ActionResult GetGoals() {
        return Ok("Get Goals");
    }

    [HttpPut("{id}")]
    public ActionResult UpdateGoal(string id) {
        return Ok("Update Goal " + id);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteGoal(string id) {
        return Ok("Delete Goal " + id);
    }
}
