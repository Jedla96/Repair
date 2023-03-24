using Repair.Menu.DatabaseMethods.EmployeeDelete;

namespace Repair.Menu;

public static class MenuDeleteEmployee
{
    public static void DeleteEmployee()
    {
        string? answer;
        do
        {
            do
            {
                Console.WriteLine("Choose the filter of the employee:");
                Console.WriteLine("\t1 - Last name\n\t2 - Code\n\t3 - ID\n\t4 - Back to menu");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3" && answer != "4");

            switch (answer)
            {
                case "1":
                    DeleteByLastName.DeleteEmployeeByLastName();

                    break;
                case "2":
                    DeleteByCode.DeleteEmployeeByCode();

                    break;
                case "3":
                    DeleteById.DeleteEmployeeById();
                    break;
                case "4":
                    break;
            }
        } while (answer != "4");
    }
}