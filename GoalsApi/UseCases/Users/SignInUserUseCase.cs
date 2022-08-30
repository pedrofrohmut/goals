using GoalsApi.DataAccess;
using GoalsApi.Dtos;
using GoalsApi.Services;

namespace GoalsApi.UseCases.Users;

public class SignInUserUseCase
{
    private readonly UserDataAccess userDataAccess;
    private readonly PasswordService passwordService;
    private readonly JwtService jwtService;

    public SignInUserUseCase(
            UserDataAccess userDataAccess, 
            PasswordService passwordService, 
            JwtService jwtService) 
    {
        this.userDataAccess = userDataAccess;
        this.passwordService = passwordService;
        this.jwtService = jwtService;
    }

    public SignedUserDto  Execute(UserCredentialsDto credentials) {
        ValidateCredentials(credentials);
        var foundUser = FindUserByEmail(credentials.Email);
        VerifyPassword(credentials.Password, foundUser.PasswordHash);
        var token = GenerateJWT(foundUser.Id);
        return new SignedUserDto() {
            Id = foundUser.Id.ToString(),
            Name = foundUser.Name,
            Email = foundUser.Email,
            Token = token
        };
    }

    private void ValidateCredentials(UserCredentialsDto credentials) {
        if (String.IsNullOrWhiteSpace(credentials.Email) || 
            String.IsNullOrWhiteSpace(credentials.Password)) {
            throw new ArgumentException("Please add all required fields for sign in");
        }
    }

    private UserDbDto FindUserByEmail(String email) {
        var user = this.userDataAccess.FindUserByEmail(email);
        if (user == null) {
            throw new Exception("User not found by e-mail");
        }
        return user;
    }

    private void VerifyPassword(string password, string hash) {
        bool isMatch = this.passwordService.ComparePasswordAndHash(password, hash);
        if (!isMatch) {
            throw new Exception("Credentials password and database password hash do not match");
        }
    }

    private string GenerateJWT(Guid id) {
        var token = this.jwtService.GenerateJWT(id);
        return token;
    }
}
