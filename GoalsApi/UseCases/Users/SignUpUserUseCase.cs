using GoalsApi.Dtos;
using GoalsApi.DataAccess;

namespace GoalsApi.UseCases.Users
{
    using BCrypt.Net;

    public class SignUpUserUseCase
    {
        private readonly UserDataAccess userDataAccess;
       
        public SignUpUserUseCase(UserDataAccess userDataAccess) {
            this.userDataAccess = userDataAccess;
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
            try {
                var hash = BCrypt.HashPassword(password);
                return hash;
            } catch (Exception e) {
                throw new Exception("Error to generate a password hash. " + e.Message);
            }
        }

        private void CreateUser(CreateUserDto newUser, string passwordHash) {
            try {
                this.userDataAccess.CreateUser(newUser, passwordHash);
            } catch (Exception e) {
                throw new Exception("Error to create an user. " + e.Message);
            }
        }
    }
}
