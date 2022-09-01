using GoalsApi.Dtos;

namespace GoalsApi.DataAccess;

public interface UserDataAccess
{
    UserDbDto? FindUserByEmail(string email);
    UserDbDto? FindUserById(Guid userId);
    void CreateUser(CreateUserDto newUser, string passwordHash);
}
