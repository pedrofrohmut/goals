using GoalsApi.DataAccess;
using GoalsApi.Dtos;
using GoalsApi.Services;

namespace GoalsApi.UseCases.Users;

public class VerifyTokenUseCase
{
    private readonly UserDataAccess userDataAccess;
    private readonly JwtService jwtService;

    public VerifyTokenUseCase(UserDataAccess userDataAccess, JwtService jwtService)
    {
        this.userDataAccess = userDataAccess;
        this.jwtService = jwtService;
    }
    
    public void Execute(string token) 
    {
        var decodedToken = GetValidatedAndDecodedToken(token);
        FindUserByTokenUserId(decodedToken.UserId);
    }

    private DecodedTokenDto GetValidatedAndDecodedToken(string token)
    {
        var decoded = jwtService.GetValidatedAndDecodedToken(token);
        if (decoded == null) {
            throw new ArgumentException("Token is not valid or it is expired");
        }
        return decoded;
    }

    private void FindUserByTokenUserId(Guid userId) 
    {
        var user = userDataAccess.FindUserById(userId);
        if (user == null) {
            throw new ArgumentException("User not found by token id");
        }
    }
}
