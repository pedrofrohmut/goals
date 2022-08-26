using GoalsApi.Dtos;

namespace GoalsApi.DataAccess
{
    public interface UserDataAccess
    {
        UserDbDto? FindUserByEmail(string email);
        void CreateUser(CreateUserDto newUser, string passwordHash);
    }
}
