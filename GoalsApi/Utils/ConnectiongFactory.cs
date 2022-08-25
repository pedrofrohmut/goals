using Npgsql;

namespace GoalsApi.Utils
{
    public class ConnectiongFactory
    {
        public async Task<NpgsqlConnection> GetConnection(string username, string password) {
            var connectionString = $"Host=localhost; Username={username}; Password={password}; Database=goals_db";
            NpgsqlConnection? connection = null;
            try {
                connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            } catch (Exception e) {
                throw new Exception("Error to connect. " + e.Message);
            } finally {
                if (connection != null) {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
