namespace GoalsApi.Services.Bcrypt;

using BCrypt.Net;

public class BCryptPasswordService : PasswordService
{

    public bool ComparePasswordAndHash(string password, string hash) {
        try {
            bool isMatch = BCrypt.Verify(password, hash);
            return isMatch;
        } catch (Exception e) {
            throw new Exception("Error to verify the password with the hash. " + e.Message);
        }
    }

    public string GetPasswordHash(string password) {
        try {
            var hash = BCrypt.HashPassword(password);
            return hash;
        } catch (Exception e) {
            throw new Exception("Error to generate a password hash. " + e.Message);
        }
    }
}
