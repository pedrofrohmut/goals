using System.Data;

using Npgsql;

namespace GoalsApi.Utils;

public class ConnectionManager
{
    public static NpgsqlConnection GetConnection(string username, string password) =>
        new NpgsqlConnection($"Host=localhost; Username={username}; Password={password}; Database=goals_db");

    public static void OpenConnection(IDbConnection connection) {
        try {
            connection.Open();
            System.Console.WriteLine("Database Connection Opened");
        } catch (System.Exception e) {
            System.Console.WriteLine(e.StackTrace);
            throw new Exception("Error to open the database connection");
        }
    }

    public static void CloseConnection(IDbConnection connection) {
        try {
            if (connection.State == ConnectionState.Open) {
                connection.Close();
            }
            System.Console.WriteLine("Database Connection Closed");
        } catch (System.Exception e) {
            System.Console.WriteLine(e.StackTrace);
            throw new Exception("Error to close the database connection");
        }

    }
}
