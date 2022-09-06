using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoalsApi.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace GoalsApi.Services.Microsoft;

public class MicrosoftJwtService : JwtService
{
    private readonly string jwtSecret;

    public MicrosoftJwtService(string jwtSecret)
    {
        this.jwtSecret = jwtSecret;
    }


    public string? GenerateToken(Guid userId)
    {
        try {
            var subject = new ClaimsIdentity(new Claim[] {
                new Claim("userId", userId.ToString())
            });
            // Must be Utc time
            var expirationDate = DateTime.UtcNow.AddDays(30);
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var descriptor = new SecurityTokenDescriptor() {
                Subject = subject,
                Expires = expirationDate,
                SigningCredentials = credentials
            };
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(descriptor);
            var token = handler.WriteToken(securityToken);
            return token;
        } catch (Exception) {
            return null;
        }
    }

    public DecodedTokenDto? GetValidatedAndDecodedToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        var parameters = new TokenValidationParameters() {
            IssuerSigningKey         = new SymmetricSecurityKey(key),
            ValidateAudience         = false,
            ValidateIssuer           = false,
            ValidateIssuerSigningKey = true,
        };
        SecurityToken? securityToken = null;
        try {
            // Validate token with env jwtSecret
            handler.ValidateToken(token, parameters, out securityToken);
        } catch (Exception e) 
          when (e is SecurityTokenExpiredException || 
                e is SecurityTokenInvalidSignatureException) {
            return null;
        }
        var decoded = securityToken as JwtSecurityToken;
        if (decoded == null) return null;
        // Keys: userId, nbf, exp, iat
        var claims = decoded.Claims
            .ToDictionary(claim => claim.Type, claim => claim.Value);
        // Validate User Id
        var isValidUserId = Guid.TryParse(claims["userId"], out var userId);
        if (!claims.ContainsKey("userId") || !isValidUserId) return null;
        // Validate exp
        if (!claims.ContainsKey("exp")) return null;
        try { 
            new DateTime(Convert.ToUInt32(claims["exp"])); 
        } catch (Exception) { 
            return null; 
        }
        return new DecodedTokenDto() {
            UserId = userId
        };
    }
}
