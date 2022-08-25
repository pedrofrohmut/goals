using GoalsApi.Dtos;

namespace GoalsApi.DataAccess
{
    public interface UserDataAccess
    {
        Task<UserDbDto> FindUserByEmail(string email);
        Task CreateUser(CreateUserDto newUser, string passwordHash);
    }
}
