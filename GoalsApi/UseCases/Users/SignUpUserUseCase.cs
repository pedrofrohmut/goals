using GoalsApi.Dtos;
using GoalsApi.DataAccess;
using GoalsApi.Services;

namespace GoalsApi.UseCases.Users;

public class SignUpUserUseCase
{
    private readonly UserDataAccess userDataAccess;
    private readonly PasswordService passwordService;

    public SignUpUserUseCase(UserDataAccess userDataAccess, PasswordService passwordService) {
        this.userDataAccess = userDataAccess;
        this.passwordService = passwordService;
    }

    public void Execute(CreateUserDto newUser) {
        ValidateNewUser(newUser);
        CheckEmailAlreadyTaken(newUser.Email);
        var passwordHash = CreatePasswordHash(newUser.Password);
        CreateUser(newUser, passwordHash);
    }

    private void ValidateNewUser(CreateUserDto newUser) {
        if (newUser.Name == "" || 
                newUser.Email == "" || 
                newUser.Password == "" || 
                newUser.Phone == "") {
            throw new ArgumentNullException("Please add all required fields for the user");
        }
    }

    private void CheckEmailAlreadyTaken(string email) {
        var user = this.userDataAccess.FindUserByEmail(email);
        if (user != null) {
            throw new ArgumentException("E-mail already taken");
        }
    }

    private string CreatePasswordHash(string password) {
        var passwordHash = this.passwordService.GetPasswordHash(password);
        return passwordHash;
    }

    private void CreateUser(CreateUserDto newUser, string passwordHash) {
        try {
            this.userDataAccess.CreateUser(newUser, passwordHash);
        } catch (Exception e) {
            throw new Exception("Error to create an user. " + e.Message);
        }
    }
}
