using Npgsql;
using Repair.Menu.DatabaseMethods.UserFind;

namespace Repair.Menu.DatabaseMethods.UserDelete;

public class DeleteByCode
{
    public static void DeleteUserByCode()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the Code of the user: ");
            string? selectAnswer = Console.ReadLine();
            int userIndex = FindIndexByCode.FindUserIndexOfCode(selectAnswer);
            if (userIndex == -1)
            {
                connection.Close();
            }
            else
            {
                NpgsqlCommand editCommand =
                    new NpgsqlCommand(
                        $"SELECT * FROM users WHERE code = '{selectAnswer}' OFFSET {userIndex - 1} LIMIT 1",
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
                // Delete the user
                Console.WriteLine(
                    "Are you sure you want to delete following user ?\n1 - Delete\n2 - Back to menu\n");
                Console.WriteLine($"ID: {userId} Code: {code}");
                Console.WriteLine($"First name: {firstName}");
                Console.WriteLine($"Last name: {lastName}");
                Console.WriteLine($"Date of birth: {dateOfBirth:dd/MM/yyyy}");
                Console.WriteLine($"Position: {position}");
                string? deleteAnswer = Console.ReadLine();
                switch (deleteAnswer)
                {
                    case "1":
                        NpgsqlCommand deleteCommand1 =
                            new NpgsqlCommand(
                                $"DELETE FROM users WHERE code = '{selectAnswer}'",
                                connection);
                        NpgsqlCommand deleteCommand2 =
                            new NpgsqlCommand(
                                $"DELETE FROM accounts WHERE code = '{selectAnswer}'",
                                connection);
                        int rowsAffected = deleteCommand1.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"User deleted successfully.");
                            deleteCommand2.ExecuteNonQuery();
                        }
                        else
                        {
                            Console.WriteLine($"Failed to delete user. Enter valid Code.");
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