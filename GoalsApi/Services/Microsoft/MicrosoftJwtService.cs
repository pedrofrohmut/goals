using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GoalsApi.Services.Microsoft
{
    public class MicrosoftJwtService : JwtService 
    {
        private readonly string jwtSecret;

        public MicrosoftJwtService(string jwtSecret) {
            this.jwtSecret = jwtSecret;
        }

        public string GenerateJWT(Guid id) {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret));
            var jst = new JwtSecurityToken(
                claims: new List<Claim>() {
                    new Claim("userId", id.ToString())
                },
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jst);
            return token;
        }
    }
}
