using Npgsql;

namespace Repair.Menu.DatabaseMethods;

public class EmployeeDataChangeByLastName
{
    public static void EmployeeChangeLastNameData()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))

        {
            connection.Open();
            Console.WriteLine("Enter the last name of the employee to edit: ");
            string selectAnswer = Console.ReadLine();

            // Find all employees with matching last names
            NpgsqlCommand selectCommand =
                new NpgsqlCommand($"SELECT * FROM employees WHERE lastname = '{selectAnswer}'", connection);
            NpgsqlDataReader reader = selectCommand.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("No employees found.");
                return;
            }

            // Display a list of matching employees and let the user choose which one to edit
            Console.WriteLine("Enter the index of the employee to edit: ");
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

            // Get the employee to edit
            NpgsqlCommand editCommand =
                new NpgsqlCommand(
                    $"SELECT * FROM employees WHERE lastname = '{selectAnswer}' OFFSET {selectedEmployeeIndex - 1} LIMIT 1",
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

            Console.WriteLine($"Editing employee {firstName} {lastName} ({code})");

            // Let the user choose what to edit
            Console.WriteLine("Choose data to edit:\n1 - First name\n2 - Last name\n3 - Date of birth\n4 - Position");
            string? editAnswer = Console.ReadLine();

            switch (editAnswer)
            {
                case "1":
                    Console.WriteLine("Enter new first name:");
                    string? newFirstName = Console.ReadLine();
                    NpgsqlCommand updateFirstNameCommand =
                        new NpgsqlCommand($"UPDATE employees SET firstname = '{newFirstName}' WHERE id = {employeeId}",
                            connection);
                    updateFirstNameCommand.ExecuteNonQuery();
                    firstName = newFirstName;
                    break;
                case "2":
                    Console.WriteLine("Enter new last name:");
                    string newLastName = Console.ReadLine();
                    NpgsqlCommand updateLastNameCommand =
                        new NpgsqlCommand($"UPDATE employees SET lastname = '{newLastName}' WHERE id = {employeeId}",
                            connection);
                    updateLastNameCommand.ExecuteNonQuery();
                    lastName = newLastName;
                    break;
                case "3":
                    var validDate = true;
                    DateTime newDateOfBirth = DateTime.MinValue;
                    while (validDate)
                    {
                        Console.WriteLine("Enter new date of birth (dd/mm/yyyy):");
                        try
                        {
                            newDateOfBirth = DateTime.Parse(Console.ReadLine());
                            DateTime.TryParse(newDateOfBirth.ToString(), out newDateOfBirth);
                            validDate = false;
                        }
                        catch
                        {
                            Console.WriteLine("Invalid date format. Please enter the date in the format dd/mm/yyyy:");
                        }
                    }

                    NpgsqlCommand updateDateOfBirthCommand = new NpgsqlCommand(
                        $"UPDATE employees SET dateofbirth = '{newDateOfBirth:yyyy-MM-dd}' WHERE id = {employeeId}",
                        connection);
                    updateDateOfBirthCommand.ExecuteNonQuery();
                    dateOfBirth = newDateOfBirth;
                    break;

                case "4":
                    Console.WriteLine("Enter new position:");
                    string newPosition = Console.ReadLine();
                    NpgsqlCommand updatePositionCommand =
                        new NpgsqlCommand($"UPDATE employees SET position = '{newPosition}' WHERE id = {employeeId}",
                            connection);
                    updatePositionCommand.ExecuteNonQuery();
                    position = newPosition;
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }

            Console.WriteLine($"Employee {firstName} {lastName} ({code}) updated:");
            Console.WriteLine($"First name: {firstName}");
            Console.WriteLine($"Last name: {lastName}");
            Console.WriteLine($"Date of birth: {dateOfBirth:dd/MM/yyyy}");
            Console.WriteLine($"Position: {position}");
            connection.Close();
        }
    }
}