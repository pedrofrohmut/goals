using GoalsApi.Dtos;

namespace GoalsApi.Services;

public interface JwtService
{
    string? GenerateToken(Guid id);
    DecodedTokenDto? GetValidatedAndDecodedToken(string token);
}
