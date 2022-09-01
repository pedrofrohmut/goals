using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GoalsApi.Entities;

public class Token
{
    public static TokenValidationParameters GetTokenValidationParameters(string jwtSecret)
    {
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        return new TokenValidationParameters() {
             IssuerSigningKey         = new SymmetricSecurityKey(key),
             ValidateAudience         = false,
             ValidateIssuer           = false,
             ValidateIssuerSigningKey = true,
             RequireExpirationTime    = true,
        };
    }

}
