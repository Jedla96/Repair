using Npgsql;
using Repair.Menu.DatabaseMethods;

namespace Repair.Login
{
    public abstract class LoginSetUp
    {
        public static void Login()
        {
            string connectionString = "Server=localhost;Port=5432;Database=postgres;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                while (true)
                {
                    Console.WriteLine("Username: ");
                    string? username = Console.ReadLine();

                    // Find user with matching username
                    NpgsqlCommand selectCommand =
                        new NpgsqlCommand($"SELECT * FROM users WHERE login = '{username}'", connection);
                    NpgsqlDataReader reader = selectCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No users found.");
                        reader.Close(); // Close the reader before continuing
                        continue;
                    }

                    reader.Read();
                    string? storedPassword =
                        reader["password"].ToString(); // Assuming password column name is "password"
                    reader.Close(); // Close the reader before continuing

                    Console.WriteLine("Password: ");
                    string? password = Console.ReadLine();

                    if (password == storedPassword)
                    {
                        Console.WriteLine("Login successful!");
                        if (username == "admin")
                        {
                            List<User> users = new List<User>();
                            Menu.MenuAdmin.Menu.MenuSetUp(users);
                        }
                        else if (username != null && password != null)
                        {
                            NpgsqlCommand selectCommand1 =
                                new NpgsqlCommand(
                                    "SELECT code FROM users WHERE login = @username AND password = @password",
                                    connection);
                            selectCommand1.Parameters.AddWithValue("username", username);
                            selectCommand1.Parameters.AddWithValue("password", password);
                            string? code = selectCommand1.ExecuteScalar()?.ToString();
                            Menu.MenuUser.Menu.MenuSetUp(code);
                        }
                        else
                        {
                            throw new Exception("Jsem kokot a je to null");
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect password. Please try again.");
                    }
                }
            }
        }
    }
}