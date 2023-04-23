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
                string insertSql1 =
                    $"INSERT INTO users (FirstName, LastName, DateOfBirth, Position, Code, Login, Password) VALUES (:FirstName, :LastName, :DateOfBirth, :Position, :Code, :Login, :Password)";
                NpgsqlCommand insertCommand1 = new NpgsqlCommand(insertSql1, connection);
                insertCommand1.Parameters.AddWithValue(":FirstName", user.FirstName!);
                insertCommand1.Parameters.AddWithValue(":LastName", user.LastName!);
                insertCommand1.Parameters.AddWithValue(":DateOfBirth", user.DateOfBirth);
                insertCommand1.Parameters.AddWithValue(":Position", user.Position!);
                insertCommand1.Parameters.AddWithValue(":Code", user.Code!);
                insertCommand1.Parameters.AddWithValue(":Login", user.Login!);
                insertCommand1.Parameters.AddWithValue(":Password", user.Password!);
                insertCommand1.ExecuteNonQuery();

               string insertSql2 = $"INSERT INTO accounts (FirstName, LastName, Code) VALUES (:FirstName, :LastName, :Code)";
                NpgsqlCommand insertCommand2 = new NpgsqlCommand(insertSql2, connection);
                insertCommand2.Parameters.AddWithValue(":FirstName", user.FirstName!);
                insertCommand2.Parameters.AddWithValue(":LastName", user.LastName!);
                insertCommand2.Parameters.AddWithValue(":Code", user.Code!);
                insertCommand2.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}