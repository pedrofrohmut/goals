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
   
    public UsersController(IConfiguration configuration) 
    {
        this.configuration = configuration;
    }

    // @desc Creates a new user
    // @route POST api/users
    // @access public
    [HttpPost]
    [AllowAnonymous]
    public ActionResult<string> SignUp([FromBody] CreateUserDto newUser) 
    {
        var connection = ConnectionManager.GetConnectionFromConfig(configuration);
        var userDataAccess = new DapperUserDataAccess(connection);
        var passwordService = new BCryptPasswordService();
        var signUpUserUseCase = new SignUpUserUseCase(userDataAccess, passwordService);
        try {
            ConnectionManager.OpenConnection(connection);
            signUpUserUseCase.Execute(newUser);
            return new ObjectResult("Usuario Criado") { StatusCode = 201 };
        } catch (ArgumentException e) {
            return new ObjectResult(e.Message) { StatusCode = 400 };
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { StatusCode = 500 };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }

    // @desc Sign In a registered user
    // @route POST api/users/signin
    // @access public
    [HttpPost("signin")]
    [AllowAnonymous]
    public ActionResult<string> SignInUser([FromBody] UserCredentialsDto credentials) 
    {
        var connection = ConnectionManager.GetConnectionFromConfig(configuration);
        var userDataAccess = new DapperUserDataAccess(connection);
        var passwordService = new BCryptPasswordService();
        var jwtSecret = this.configuration["jwtSecret"];
        var jwtService = new MicrosoftJwtService(jwtSecret);
        var signInUserUseCase = new SignInUserUseCase(userDataAccess, passwordService, jwtService);
        try {
            ConnectionManager.OpenConnection(connection);
            var signedUser = signInUserUseCase.Execute(credentials);
            return new ObjectResult(signedUser) { StatusCode = 200 };
        } catch (ArgumentException e) {
            return new ObjectResult(e.Message) { StatusCode = 400 };
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { StatusCode = 500 };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }


    // @desc Verify if token is valid
    // @route POST api/users/verify
    // @access public
    [HttpGet("verify")]
    [AllowAnonymous]
    public ActionResult<bool> VerifyToken([FromHeader] string authorization) 
    {
        var token = authorization.Split(" ")[1];
        var connection = ConnectionManager.GetConnectionFromConfig(configuration);
        var userDataAccess = new DapperUserDataAccess(connection);
        var jwtSecret = this.configuration["jwtSecret"];
        var jwtService = new MicrosoftJwtService(jwtSecret);
        var verifyTokenUseCase = new VerifyTokenUseCase(userDataAccess, jwtService);
        try {
            ConnectionManager.OpenConnection(connection);
            verifyTokenUseCase.Execute(token);
            return new ObjectResult(true) { StatusCode = 200 };
        } catch (ArgumentException) {
            return new ObjectResult(false) { StatusCode = 200 };
        } catch (Exception e) {
            return new ObjectResult("Some other error, " + e.Message) { StatusCode = 500 };
        } finally {
            ConnectionManager.CloseConnection(connection);
        }
    }
}
