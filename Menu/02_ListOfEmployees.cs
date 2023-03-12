using Npgsql;
using Repair.Menu.DatabaseMethods;

namespace Repair.Menu;

public static class MenuListOfEmployees
{
    public static void ListOfEmployees()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM employees", connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string firstName = reader.GetString(0);
                string lastName = reader.GetString(1);
                DateTime dateOfBirth = reader.GetDateTime(2);
                string position = reader.GetString(3);
                string code = reader.GetString(4);
                int id = reader.GetInt32(5);

                Console.WriteLine(
                    $"ID: {id}\nName: {firstName} {lastName}\nDate of birth: {dateOfBirth.ToString("dd/MM/yyyy")}\nPosition: {position} - Code: {code}\n");

                connection.Close();
            }
        }
    }
}