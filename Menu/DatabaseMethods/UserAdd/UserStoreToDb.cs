using Npgsql;

namespace Repair.Menu.DatabaseMethods.UserAdd;

public abstract class UserStoreToDb
{
    public static void StoreUserToDatabase(List<User> users, int index)
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            User user = users[index];

            {
                string insertSql =
                    $"INSERT INTO users (FirstName, LastName, DateOfBirth, Position, Code, Login, Password) VALUES (:FirstName, :LastName, :DateOfBirth, :Position, :Code, :Login, :Password)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, connection);
                insertCommand.Parameters.AddWithValue(":FirstName", user.FirstName!);
                insertCommand.Parameters.AddWithValue(":LastName", user.LastName!);
                insertCommand.Parameters.AddWithValue(":DateOfBirth", user.DateOfBirth);
                insertCommand.Parameters.AddWithValue(":Position", user.Position!);
                insertCommand.Parameters.AddWithValue(":Code", user.Code!);
                insertCommand.Parameters.AddWithValue(":Login", user.Login!);
                insertCommand.Parameters.AddWithValue(":Password", user.Password!);
                insertCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}