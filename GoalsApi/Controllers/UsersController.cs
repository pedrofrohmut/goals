using GoalsApi.DataAccess.Dapper;
using GoalsApi.Dtos;
using GoalsApi.UseCases;
using GoalsApi.UseCases.Users;
using GoalsApi.Utils;

using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersUseCases usersUseCases;
    private readonly IConfiguration configuration;
   
    public UsersController(IConfiguration configuration) {
        this.usersUseCases = new UsersUseCases();
        this.configuration = configuration;
    }
     
    [HttpPost]
    public async Task<ActionResult<string>> SignUp(CreateUserDto newUser) {
        string DB_USER = this.configuration["username"];
        string DB_PASS = this.configuration["password"];
        var connection = await new ConnectiongFactory().GetConnection(DB_USER, DB_PASS);
        var userDataAccess = new DapperUserDataAccess(connection);
        try {
            var useCase = new SignUpUserUseCase(userDataAccess, connection);
            useCase.Execute(newUser);
            return new ObjectResult("Usuario Criado") { StatusCode = StatusCodes.Status201Created };
        } catch (ArgumentNullException e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("signin")]
    public async Task<ActionResult<string>> SignInUser() {
        var msg = await this.usersUseCases.SignIn();
        return Ok(msg);
    }
}
