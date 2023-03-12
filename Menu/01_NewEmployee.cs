using Repair.Menu.EmployeesMethods;
using Repair.Menu.DatabaseMethods;

namespace Repair.Menu;

public static class MenuNewEmployee
{
    public static void NewEmployee(List<Employee> employees)
    {
        string code = EmployeesMethods.IdGenerator.GenerateId();
        Console.WriteLine("Here is the ID of a new employee: " + code + "\nFill the following: ");

        employees.Add(new Employee(string.Empty, string.Empty, DateTime.MinValue, string.Empty,
            code));
        int index = employees.FindIndex(e => e.Code == code);
        if (index >= 0)
        {
            EmployeeDataChoose.FirstNameChoose(employees, index);
            EmployeeDataChoose.LastNameChoose(employees, index);
            EmployeeDataChoose.DateChoose(employees, index);
            EmployeeDataChoose.PositionChoose(employees, index);
            EmployeeStoreToDb.StoreEmployeeToDatabase(employees,index);
        }
        Console.WriteLine(
            "New employee was successfully added. Do you wanna see list of all employees ?\n1: Yes\n2: No");
        string? listAnswer = Console.ReadLine();
        switch (listAnswer)
        {
            case "1":
                MenuListOfEmployees.ListOfEmployees();
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }
    }
}