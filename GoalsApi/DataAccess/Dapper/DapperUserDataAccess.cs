using System.Data;

using Dapper;

using GoalsApi.Dtos;

namespace GoalsApi.DataAccess.Dapper;

public class DapperUserDataAccess : UserDataAccess 
{
    private readonly IDbConnection connection;

    public DapperUserDataAccess(IDbConnection connection) 
    {
        this.connection = connection;
    }

    private UserDbDto MapUserRowToDto(dynamic row) => new UserDbDto {
        Id = row.id,
        Name = row.name,
        Email = row.email,
        PasswordHash = row.password_hash,
        Phone = row.phone
    };

    public UserDbDto? FindUserByEmail(string email) 
    {
        var sql = "SELECT * FROM users WHERE email = @email";
        var row = this.connection.Query(sql, new { email }).SingleOrDefault();
        if (row == null) return null;
        var user = MapUserRowToDto(row);
        return user;
    }

    public void CreateUser(CreateUserDto newUser, string passwordHash) 
    {
        var sql = "INSERT INTO users (name, email, phone, password_hash) VALUES (@name, @email, @phone, @passwordHash)";
        this.connection.Query(sql, new {
             @name = newUser.Name,
             @email = newUser.Email,
             @phone = newUser.Phone,
             @passwordHash = passwordHash
        });
    }

    public UserDbDto? FindUserById(Guid userId) 
    {
        var sql = "SELECT * FROM users WHERE id = @userId";
        var row = this.connection.Query(sql, new { userId }).SingleOrDefault();
        if (row == null) return null;
        var user = MapUserRowToDto(row);
        return user;
    }
}
