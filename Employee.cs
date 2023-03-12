
namespace Repair;

public class Employee
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBrth { get; set; }
    public string? Position { get; set; }
    public string? Code { get; }

    public Employee(string firstName, string lastName, DateTime dateOfBrth, string position, string code)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBrth = dateOfBrth;
        Position = position;
        Code = code;
    }
}