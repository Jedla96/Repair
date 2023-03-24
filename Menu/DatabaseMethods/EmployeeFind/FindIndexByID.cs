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

            // Find all employees with matching id
            NpgsqlCommand selectCommand =
                new NpgsqlCommand($"SELECT * FROM employees WHERE id = '{id}'", connection);
            NpgsqlDataReader reader = selectCommand.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("No employees found.");
                return -1;
            }

            // Display a list of matching employees and let the user choose which one to edit
            Console.WriteLine("Employee found: \n");
            int employeeIndex = 1;
            reader.Read();
            
            string firstNameFind = reader.GetString(reader.GetOrdinal("firstname"));
            string lastNameFind = reader.GetString(reader.GetOrdinal("lastname"));
            string codeFind = reader.GetString(reader.GetOrdinal("code"));

            Console.WriteLine($"{firstNameFind} {lastNameFind} ({codeFind})\n");

            reader.Close();
            
            return employeeIndex;
        }
    }
}