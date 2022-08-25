using GoalsApi.Dtos;
using GoalsApi.DataAccess;
using Dapper;
using Npgsql;

namespace GoalsApi.UseCases.Users
{
    // using BCrypt.Net;
    public class SignUpUserUseCase
    {
        private readonly UserDataAccess userDataAccess;
        private readonly NpgsqlConnection connection;
       
        public SignUpUserUseCase(UserDataAccess userDataAccess, NpgsqlConnection connection) {
            this.userDataAccess = userDataAccess;
            this.connection = connection;
        }

        public async void Execute(CreateUserDto newUser) {
            // var sql1 = "INSERT INTO users (name, email, phone, password_hash) VALUES (@name, @email, @phone, @passwordHash)";
            // this.connection.Query(sql1, new {
            //     @name = newUser.Name,
            //     @email = newUser.Email,
            //     @phone = newUser.Phone,
            //     @passwordHash = "SUPER_HASH"
            // });

            var users = this.connection.Query("SELECT * FROM users");
            System.Console.WriteLine(users);
            foreach (var user in users)
            {
               System.Console.WriteLine(user); 
            }
           
            // var sql = "SELECT * FROM users WHERE email = @email"; 
            // var user = this.connection.Query<UserDbDto>(sql, new { @email = newUser.Email }).SingleOrDefault();
            // System.Console.WriteLine(user);
           
           
           
            // ValidateNewUser(newUser);
            // CheckUserExists(newUser.Email);
            // var passwordHash = CreatePasswordHash(newUser.Password);
            // CreatesUser(newUser, passwordHash);
        }

        // private void ValidateNewUser(CreateUserDto newUser) {
        //     if (newUser.Name == "" || 
        //         newUser.Email == "" || 
        //         newUser.Password == "" || 
        //         newUser.Phone == "") {
        //         throw new ArgumentNullException("Please add all required fields for the user");
        //     }
        // }
        //
        // private async void CheckUserExists(string newUserEmail) {
        //     var userExists = (await this.userDataAccess.FindUserByEmail(newUserEmail)) != null;
        //     if (userExists) {
        //         throw new Exception("User already registered with this e-mail");
        //     }
        // }
        //
        // private string CreatePasswordHash(string password) {
        //     try {
        //         var hash = BCrypt.HashPassword(password);
        //         return hash;
        //     } catch (Exception e) {
        //         throw new Exception("Error to generate a password hash. " + e.Message);
        //     }
        // }
        //
        // private async void CreatesUser(CreateUserDto newUser, string passwordHash) {
        //     try {
        //         await this.userDataAccess.CreateUser(newUser, passwordHash);
        //     } catch (Exception e) {
        //         throw new Exception("Error to create an user. " + e.Message);
        //     }
        // }
    }
}
