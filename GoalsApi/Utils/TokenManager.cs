using GoalsApi.Services.Microsoft;

namespace GoalsApi.Utils;

public class TokenManager
{
    public static Guid GetUserIdFromToken(IConfiguration configuration, string authorizationHeader)
    {
        var token = authorizationHeader.Split(" ")[1];
        var jwtService = new  MicrosoftJwtService(configuration["jwtSecret"]);
        var decoded = jwtService.GetValidatedAndDecodedToken(token);
        if (decoded == null) throw new ArgumentNullException("Null decoded token");
        var authUserId = decoded.UserId;
        return authUserId;
    }
}
