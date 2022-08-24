using GoalsDomain.UseCases;

using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersUseCases usersUseCases;
   
    public UsersController()
    {
        this.usersUseCases = new UsersUseCases();
    }

    [HttpPost]
    public async Task<ActionResult<string>> SignUp() {
        var msg = await this.usersUseCases.SignUp();
        return Ok(msg);
    }

    [HttpPost("signin")]
    public async Task<ActionResult<string>> SignInUser() {
        var msg = await this.usersUseCases.SignIn();
        return Ok(msg);
    }
}
