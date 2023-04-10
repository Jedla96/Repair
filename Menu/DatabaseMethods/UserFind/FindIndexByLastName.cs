using Npgsql;

namespace Repair.Menu.DatabaseMethods.UserFind;

public class FindIndexByLastName
{
    public static int FindUserIndexOfLastName(string? lastName)
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))

        {
            connection.Open();

            // Find all users with matching last names
            NpgsqlCommand selectCommand =
                new NpgsqlCommand($"SELECT * FROM users WHERE lastname = '{lastName}'", connection);
            NpgsqlDataReader reader = selectCommand.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("No users found.");
                return -1;
            }

            // Display a list of matching users and let the user choose which one to edit
            Console.WriteLine("Enter the index of the user: ");
            int i = 1;
            while (reader.Read())
            {
                string firstNameFind = reader.GetString(reader.GetOrdinal("firstname"));
                string lastNameFind = reader.GetString(reader.GetOrdinal("lastname"));
                string codeFind = reader.GetString(reader.GetOrdinal("code"));

                Console.WriteLine($"{i} - {firstNameFind} {lastNameFind} ({codeFind})");
                i++;
            }

            reader.Close();

            int userIndex;
            do
            {
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out userIndex) ||
                    userIndex < 1 ||
                    userIndex > i - 1)
                {
                    Console.WriteLine($"Invalid index. Please enter a number between 1 and {i - 1}.");
                }
                else
                {
                    break;
                }
            } while (true);

            return userIndex;
        }
    }
}