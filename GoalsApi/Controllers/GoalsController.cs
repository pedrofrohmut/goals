using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    // @Desc Add a Goal
    // @Route POST api/goals
    // @Access private
    [HttpPost]
    public ActionResult AddGoal()
    {
        return Ok();
    }

    // @Desc Get all goals from user
    // @Route GET api/goals/user/123
    // @Access private
    [HttpGet("user/{userId}")]
    public ActionResult GetGoals(Guid userId)
    {
        return Ok();
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
