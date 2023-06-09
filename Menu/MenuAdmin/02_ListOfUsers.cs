using Npgsql;

namespace Repair.Menu.MenuAdmin;

public static class MenuListOfUsers
{
    public static void ListOfUsers()
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM users ORDER BY Id", connection);
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
            }
            connection.Close();
        }
    }
}