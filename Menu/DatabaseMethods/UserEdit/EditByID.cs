using Npgsql;
using Repair.Menu.DatabaseMethods.UserFind;

namespace Repair.Menu.DatabaseMethods.UserEdit;

public class EditById
{
    public static void EditUserDataByIndex()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the ID of the user: ");
            int selectAnswer = 1;
            bool validInput = false;
            while (!validInput)
            {
                string answer = Console.ReadLine();

                if (int.TryParse(answer, out selectAnswer))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
            // Get the user to edit
            int userIndex = FindIndexById.FindUserIndexOfId(selectAnswer.ToString());
            if (userIndex == -1)
            {
                connection.Close();
            }
            else
            {
                NpgsqlCommand editCommand =
                    new NpgsqlCommand(
                        $"SELECT * FROM users WHERE id = '{selectAnswer}' OFFSET {userIndex - 1} LIMIT 1",
                        connection);
                NpgsqlDataReader editReader = editCommand.ExecuteReader();
                editReader.Read();

                int userId = editReader.GetInt32(editReader.GetOrdinal("id"));
                string firstName = editReader.GetString(editReader.GetOrdinal("firstname"));
                string lastName = editReader.GetString(editReader.GetOrdinal("lastname"));
                DateTime dateOfBirth = editReader.GetDateTime(editReader.GetOrdinal("dateofbirth"));
                string position = editReader.GetString(editReader.GetOrdinal("position"));
                string code = editReader.GetString(editReader.GetOrdinal("code"));

                editReader.Close();
                
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
                                $"UPDATE users SET firstname = '{newFirstName}' WHERE id = {userId}",
                                connection);
                        updateFirstNameCommand.ExecuteNonQuery();
                        firstName = newFirstName;
                        break;
                    case "2":
                        Console.WriteLine("Enter new last name:");
                        string newLastName = Console.ReadLine();
                        NpgsqlCommand updateLastNameCommand =
                            new NpgsqlCommand(
                                $"UPDATE users SET lastname = '{newLastName}' WHERE id = {userId}",
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
                            $"UPDATE users SET dateofbirth = '{newDateOfBirth:yyyy-MM-dd}' WHERE id = {userId}",
                            connection);
                        updateDateOfBirthCommand.ExecuteNonQuery();
                        dateOfBirth = newDateOfBirth;
                        break;

                    case "4":
                        Console.WriteLine("Enter new position:");
                        string newPosition = Console.ReadLine();
                        NpgsqlCommand updatePositionCommand =
                            new NpgsqlCommand(
                                $"UPDATE users SET position = '{newPosition}' WHERE id = {userId}",
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
                    Console.WriteLine($"User {firstName} {lastName} ({code}) updated:");
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