using Npgsql;

namespace Repair.Menu.MenuUser
{
    public abstract class AccInfo
    {
        public static void BankAccountInfo(string? code)
        {
            string connectionString = "Server=localhost;Port=5432;Database=postgres;";
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var cmd =
                new NpgsqlCommand("SELECT firstname, lastname, account, money FROM accounts WHERE code = @code",
                    connection);
            if (code != null)
            {
                cmd.Parameters.AddWithValue("code", code); 
            }

            using NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string firstName = reader.GetString(0);
                    string lastName = reader.GetString(1);
                    string account = reader.GetString(2);
                    decimal money = reader.GetDecimal(3);

                    Console.WriteLine(
                        $"Name: {firstName} {lastName}\nBank account: {account}\nMoney: {money}\n");
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            connection.Close();
        }
    }
}