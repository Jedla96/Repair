using Npgsql;

namespace Repair.Menu.DatabaseMethods.EmployeeEdit;

public class EditById
{
    public static void EditEmployeeDataByIndex()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the ID of the employee: ");
            string? selectAnswer = Console.ReadLine();

            // Get the employee to edit
            int selectedEmployeeIndex = EmployeeFind.FindIndexById.FindEmployeeIndexOfId(selectAnswer);
            if (selectedEmployeeIndex == -1)
            {
                connection.Close();
            }
            else
            {
                NpgsqlCommand editCommand =
                    new NpgsqlCommand(
                        $"SELECT * FROM employees WHERE id = '{selectAnswer}' OFFSET {selectedEmployeeIndex - 1} LIMIT 1",
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
                Console.WriteLine(
                    "Choose data to edit:\n1 - First name\n2 - Last name\n3 - Date of birth\n4 - Position");
                string? editAnswer = Console.ReadLine();

                switch (editAnswer)
                {
                    case "1":
                        Console.WriteLine("Enter new first name:");
                        string? newFirstName = Console.ReadLine();
                        NpgsqlCommand updateFirstNameCommand =
                            new NpgsqlCommand(
                                $"UPDATE employees SET firstname = '{newFirstName}' WHERE id = {employeeId}",
                                connection);
                        updateFirstNameCommand.ExecuteNonQuery();
                        firstName = newFirstName;
                        break;
                    case "2":
                        Console.WriteLine("Enter new last name:");
                        string newLastName = Console.ReadLine();
                        NpgsqlCommand updateLastNameCommand =
                            new NpgsqlCommand(
                                $"UPDATE employees SET lastname = '{newLastName}' WHERE id = {employeeId}",
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
                                Console.WriteLine(
                                    "Invalid date format. Please enter the date in the format dd/mm/yyyy:");
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
                            new NpgsqlCommand(
                                $"UPDATE employees SET position = '{newPosition}' WHERE id = {employeeId}",
                                connection);
                        updatePositionCommand.ExecuteNonQuery();
                        position = newPosition;
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
                if (editAnswer == "1" || editAnswer == "2" || editAnswer == "3" || editAnswer == "4")
                {
                    Console.WriteLine($"Employee {firstName} {lastName} ({code}) updated:");
                    Console.WriteLine($"First name: {firstName}");
                    Console.WriteLine($"Last name: {lastName}");
                    Console.WriteLine($"Date of birth: {dateOfBirth:dd/MM/yyyy}");
                    Console.WriteLine($"Position: {position}");
                }
            }
            connection.Close();
        }
    }
}