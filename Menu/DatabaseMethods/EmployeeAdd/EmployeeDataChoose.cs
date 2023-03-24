namespace Repair.Menu.DatabaseMethods.EmployeeAdd;

public static class EmployeeDataChoose
{
    public static void FirstNameChoose(List<Employee> employees, int index)
    {
        Employee employee = employees[index];
        do
        {
            Console.WriteLine("Fist name: ");
            employee.FirstName = Console.ReadLine();
        } while (string.IsNullOrEmpty(employee.FirstName));
    }

    public static void LastNameChoose(List<Employee> employees, int index)
    {
        Employee employee = employees[index];
        do
        {
            Console.WriteLine("Last name: ");
            employee.LastName = Console.ReadLine();
        } while (string.IsNullOrEmpty(employee.LastName));
    }

    public static void DateChoose(List<Employee> employees, int index)
    {
        Employee employee = employees[index];
        var validDate = true;
        while (validDate)
        {
            Console.WriteLine("Date of birth in format dd/MM/yyyy: ");

            try
            {
                employee.DateOfBrth = DateTime.Parse(Console.ReadLine());
                DateTime.TryParse(employee.DateOfBrth.ToString(), out var resultDateTime);
                employee.DateOfBrth = resultDateTime;
                validDate = false;
            }
            catch
            {
                Console.WriteLine(
                    "Invalid date format. Please enter a valid date in format dd/MM/yyyy: ");
            }
        }
    }

    public static void PositionChoose(List<Employee> employees, int index)
    {
        Employee employee = employees[index];
        do
        {
            Console.WriteLine("Position: ");
            employee.Position = Console.ReadLine();
        } while (string.IsNullOrEmpty(employee.Position));
    }
}