using System.Globalization;

namespace Repair.Menu.DatabaseMethods.UserAdd;

public static class UserDataChoose
{
    public static void FirstNameChoose(List<User> users, int index)
    {
        User user = users[index];
        do
        {
            Console.WriteLine("Fist name: ");
            user.FirstName = Console.ReadLine();
        } while (string.IsNullOrEmpty(user.FirstName));
    }

    public static void LastNameChoose(List<User> users, int index)
    {
        User user = users[index];
        do
        {
            Console.WriteLine("Last name: ");
            user.LastName = Console.ReadLine();
        } while (string.IsNullOrEmpty(user.LastName) || user.LastName.Length <3 );
    }
    public static void DateChoose(List<User> users, int index)
    {
        User user = users[index];
        var validDate = true;
        while (validDate)
        {
            Console.WriteLine("Date of birth in format dd/MM/yyyy: ");

            try
            {
                user.DateOfBirth = DateTime.Parse(Console.ReadLine()!);
                DateTime.TryParse(user.DateOfBirth.ToString(CultureInfo.CurrentCulture), out var resultDateTime);
                user.DateOfBirth = resultDateTime;
                validDate = false;
            }
            catch
            {
                Console.WriteLine(
                    "Invalid date format. Please enter a valid date in format dd/MM/yyyy: ");
            }
        }
    }
    public static void PositionChoose(List<User> users, int index)
    {
        User user = users[index];
        do
        {
            Console.WriteLine("Position: ");
            user.Position = Console.ReadLine();
        } while (string.IsNullOrEmpty(user.Position));
    }
    public static void LoginCreate(List<User> users, int index)
    {
        User user = users[index];
        string? firstName = user.FirstName;
        string? lastName = user.LastName;
        string? code = user.Code;

        string login = string.Concat(firstName?[0], lastName?.Substring(0, Math.Min(3, lastName.Length)), code);
        user.Login = login;
    }
    public static void PasswordChoose(List<User> users, int index)
    {
        User user = users[index];
        do
        {
            Console.WriteLine("Password (4 - 16 symbols): ");
            user.Password = Console.ReadLine();
        } while (string.IsNullOrEmpty(user.Password) || user.Password.Length is < 4 or > 16);
    }
}