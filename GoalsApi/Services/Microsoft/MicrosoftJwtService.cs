using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace GoalsApi.Services.Microsoft;

public class MicrosoftJwtService : JwtService 
{
    private readonly string jwtSecret;

    public MicrosoftJwtService(string jwtSecret) {
        this.jwtSecret = jwtSecret;
    }


    public string GenerateJWT(Guid userId) 
    {
        try {
            var subject = new ClaimsIdentity(new Claim[] {
                new Claim("userId", userId.ToString())
            });
            var expirationDate = DateTime.Now.AddDays(30);
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
        } catch (Exception e) {
            System.Console.WriteLine("Error to generate token. " + e.Message);
            System.Console.WriteLine(e.StackTrace);
            throw e;
        }
    }
}
