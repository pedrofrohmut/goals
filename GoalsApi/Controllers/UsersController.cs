using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public ActionResult AddUser() {
        return Ok("Add User");
    }

    [HttpPost("signin")]
    public ActionResult SignInUser() {
        return Ok("Sign In User");
    }
}
