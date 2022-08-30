using System.Data;

using GoalsApi.DataAccess.Dapper;
using GoalsApi.Dtos;
using GoalsApi.Services.Bcrypt;
using GoalsApi.Services.Microsoft;
using GoalsApi.UseCases.Users;
using GoalsApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoalsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration configuration;
   
    public UsersController(IConfiguration configuration) {
        this.configuration = configuration;
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
    [AllowAnonymous]
    public ActionResult<string> SignUp(CreateUserDto newUser) {
        var connection = GetConnection();
        var userDataAccess = new DapperUserDataAccess(connection);
        var passwordService = new BCryptPasswordService();
        var signUpUserUseCase = new SignUpUserUseCase(userDataAccess, passwordService);
        try {
            ConnectiongManager.OpenConnection(connection);
            signUpUserUseCase.Execute(newUser);
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
    [AllowAnonymous]
    public ActionResult<string> SignInUser([FromBody] UserCredentialsDto credentials) {
        var connection = GetConnection();
        var userDataAccess = new DapperUserDataAccess(connection);
        var passwordService = new BCryptPasswordService();
        var jwtSecret = this.configuration["jwtSecret"];
        var jwtService = new MicrosoftJwtService(jwtSecret);
        var signInUserUseCase = new SignInUserUseCase(userDataAccess, passwordService, jwtService);
        try {
            ConnectiongManager.OpenConnection(connection);
            var signedUser = signInUserUseCase.Execute(credentials);
            return Ok(signedUser);
        } catch (ArgumentException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return BadRequest(e.Message);
        } finally {
            ConnectiongManager.CloseConnection(connection);
        }
    }
}
