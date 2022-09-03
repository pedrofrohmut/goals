using System.Data;

using Npgsql;

namespace GoalsApi.Utils;

public class ConnectionManager
{
    public static NpgsqlConnection GetConnectionFromConfig(IConfiguration configuration) =>
        GetConnection(configuration["username"], configuration["password"]);

    public static NpgsqlConnection GetConnection(string username, string password) =>
        new NpgsqlConnection($"Host=localhost; Username={username}; Password={password}; Database=goals_db");

    public static void OpenConnection(IDbConnection connection) 
    {
        try {
            connection.Open();
        } catch (System.Exception e) {
            throw new Exception("Error to open the database connection");
        }
    }

    public static void CloseConnection(IDbConnection connection) 
    {
        try {
            if (connection.State == ConnectionState.Open) connection.Close();
        } catch (System.Exception e) {
            throw new Exception("Error to close the database connection");
        }

    }
}
