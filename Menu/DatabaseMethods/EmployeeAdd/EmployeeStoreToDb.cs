using Npgsql;

namespace Repair.Menu.DatabaseMethods.EmployeeAdd;

public class EmployeeStoreToDb
{
    public static void StoreEmployeeToDatabase(List<Employee> employees, int index)
    {
        string connectionString = "Server=localhost;Port=5432;Database=postgres;";
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Employee employee = employees[index];

            {
                string insertSql =
                    "INSERT INTO employees (FirstName, LastName, DateOfBirth, Position, Code) VALUES (:FirstName, :LastName, :DateOfBirth, :Position, :Code)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, connection);
                insertCommand.Parameters.AddWithValue(":FirstName", employee.FirstName);
                insertCommand.Parameters.AddWithValue(":LastName", employee.LastName);
                insertCommand.Parameters.AddWithValue(":DateOfBirth", employee.DateOfBrth);
                insertCommand.Parameters.AddWithValue(":Position", employee.Position);
                insertCommand.Parameters.AddWithValue(":Code", employee.Code);
                insertCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}