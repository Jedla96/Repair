using Npgsql;

namespace Repair.Menu.DatabaseMethods.UserFind;

public abstract class FindIndexByCode
{
    public static int FindUserIndexOfCode(string? code)
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))

        {
            connection.Open();

            // Find all users with matching code
            NpgsqlCommand selectCommand =
                new NpgsqlCommand($"SELECT * FROM users WHERE code = '{code}'", connection);
            NpgsqlDataReader reader = selectCommand.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("No users found.");
                return -1;
            }

            // Display a list of matching users and let the user choose which one to edit
            Console.WriteLine("User found: \n");
            int userIndex = 1;
            reader.Read();

            string firstNameFind = reader.GetString(reader.GetOrdinal("firstname"));
            string lastNameFind = reader.GetString(reader.GetOrdinal("lastname"));
            string codeFind = reader.GetString(reader.GetOrdinal("code"));

            Console.WriteLine($"{firstNameFind} {lastNameFind} ({codeFind})\n");

            reader.Close();

            connection.Close();
            return userIndex;
        }
    }
}