using System.Data;

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
        this.configuration = configuration;
        this.usersUseCases = new UsersUseCases();
    }

    private IDbConnection GetConnection() {
        string DB_USER = this.configuration["username"];
        string DB_PASS = this.configuration["password"];
        var connection = ConnectiongManager.GetConnection(DB_USER, DB_PASS);
        return connection;
    }
     
    // @desc Creates a new user
    // @route POST api/users
    // @access public
    [HttpPost]
    public ActionResult<string> SignUp(CreateUserDto newUser) {
        var connection = GetConnection();
        var userDataAccess = new DapperUserDataAccess(connection);
        try {
            ConnectiongManager.OpenConnection(connection);
            var useCase = new SignUpUserUseCase(userDataAccess);
            useCase.Execute(newUser);
            return new ObjectResult("Usuario Criado") { StatusCode = StatusCodes.Status201Created };
        } catch (ArgumentException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { 
                StatusCode = StatusCodes.Status500InternalServerError 
            };
        } finally {
            ConnectiongManager.CloseConnection(connection);
        }
    }

    // @desc Sign In a registered user
    // @route POST api/users/signin
    // @access public
    [HttpPost("signin")]
    public async Task<ActionResult<string>> SignInUser() {
        var msg = await this.usersUseCases.SignIn();
        return Ok(msg);
    }
}
