using System.Data;

using GoalsApi.Dtos;

namespace GoalsApi.DataAccess.Dapper
{
    public class DapperUserDataAccess : UserDataAccess 
    {
        private readonly IDbConnection connection;

        public DapperUserDataAccess(IDbConnection connection) {
            this.connection = connection;
        }
       
        public Task CreateUser(CreateUserDto newUser, string passwordHash) {
            throw new NotImplementedException();
        }

        public Task<UserDbDto> FindUserByEmail(string email) {
            throw new NotImplementedException();
        }
    }
}
