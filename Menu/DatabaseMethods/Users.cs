namespace Repair.Menu.DatabaseMethods;

public class User
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Position { get; set; }
    public string? Code { get; }
    public string? Login { get; set; }
    public string? Password { get; set; }

    public User(string firstName, string lastName, DateTime dateOfBirth, string position, string code)

    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Position = position;
        Code = code;
    }
}