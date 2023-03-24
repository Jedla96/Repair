
using Repair.Menu.DatabaseMethods.EmployeeAdd;

namespace Repair.Menu;

public static class Menu
{
    public static void MenuSetUp(List<Employee> employees)
    {
        string ?answer;
        do
        {
            do
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1 - Add new employee\n2 - See all employees\n3 - Edit employee data\n4 - Delete employee\n5 - Exit");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3" && answer != "4" && answer != "5");

            switch (answer)
            {
                case "1":
                    MenuNewEmployee.NewEmployee(employees);
                    break;
                case "2":
                    MenuListOfEmployees.ListOfEmployees();
                    break;
                case "3":
                    MenuEditEmployee.EditEmployee();
                    break;
                case "4":
                    MenuDeleteEmployee.DeleteEmployee();
                    break;
                case "5":
                    break;
                default:
                    MenuSetUp(employees);
                    break;
            }
        } while (answer != "5");
    }
}