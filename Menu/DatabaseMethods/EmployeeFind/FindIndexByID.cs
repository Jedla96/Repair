using Npgsql;

namespace Repair.Menu.DatabaseMethods.EmployeeFind;

public class FindIndexById
{
    public static int FindEmployeeIndexOfId(string? id)
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))

        {
            connection.Open();

            // Find all employees with matching last names
            NpgsqlCommand selectCommand =
                new NpgsqlCommand($"SELECT * FROM employees WHERE id = '{id}'", connection);
            NpgsqlDataReader reader = selectCommand.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("No employees found.");
                return -1;
            }

            // Display a list of matching employees and let the user choose which one to edit
            Console.WriteLine("Enter the index of the employee: ");
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

            int selectedEmployeeIndex;
            do
            {
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out selectedEmployeeIndex) ||
                    selectedEmployeeIndex < 1 ||
                    selectedEmployeeIndex > i - 1)
                {
                    Console.WriteLine($"Invalid index. Please enter a number between 1 and {i - 1}.");
                }
                else
                {
                    break;
                }
            } while (true);

            return selectedEmployeeIndex;
        }
    }
}