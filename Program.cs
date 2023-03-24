using Repair.Menu.DatabaseMethods.EmployeeAdd;

namespace Repair
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            Menu.Menu.MenuSetUp(employees);
        }
    }
}
