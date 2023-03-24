using Npgsql;

namespace Repair.Menu.DatabaseMethods.EmployeeDelete;

public class DeleteByCode
{
    public static void DeleteEmployeeByCode()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the Code of the employee: ");
            string? selectAnswer = Console.ReadLine();
            int selectedEmployeeIndex = EmployeeFind.FindIndexByCode.FindEmployeeIndexOfCode(selectAnswer);
            if (selectedEmployeeIndex == -1)
            {
                connection.Close();
            }
            else
            {
                NpgsqlCommand editCommand =
                    new NpgsqlCommand(
                        $"SELECT * FROM employees WHERE code = '{selectAnswer}' OFFSET {selectedEmployeeIndex - 1} LIMIT 1",
                        connection);
                NpgsqlDataReader editReader = editCommand.ExecuteReader();
                editReader.Read();

                int employeeId = editReader.GetInt32(editReader.GetOrdinal("id"));
                string firstName = editReader.GetString(editReader.GetOrdinal("firstname"));
                string lastName = editReader.GetString(editReader.GetOrdinal("lastname"));
                DateTime dateOfBirth = editReader.GetDateTime(editReader.GetOrdinal("dateofbirth"));
                string position = editReader.GetString(editReader.GetOrdinal("position"));
                string code = editReader.GetString(editReader.GetOrdinal("code"));

                editReader.Close();
                // Delete the employee
                Console.WriteLine(
                    "Are you sure you want to delete following employee ?\n1 - Delete\n2 - Back to menu\n");
                Console.WriteLine($"ID: {employeeId} Code: {code}");
                Console.WriteLine($"First name: {firstName}");
                Console.WriteLine($"Last name: {lastName}");
                Console.WriteLine($"Date of birth: {dateOfBirth:dd/MM/yyyy}");
                Console.WriteLine($"Position: {position}");
                string? deleteAnswer = Console.ReadLine();
                switch (deleteAnswer)
                {
                    case "1":
                        NpgsqlCommand deleteCommand =
                            new NpgsqlCommand(
                                $"DELETE FROM employees WHERE code = '{selectAnswer}'",
                                connection);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Employee deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to delete employee. Enter valid Code.");
                        }

                        break;
                    case "2":
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
            connection.Close();
        }
    }
}