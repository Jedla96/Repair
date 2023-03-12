using Repair.Menu.DatabaseMethods;
using Repair.Menu.EmployeesMethods;

namespace Repair.Menu;

public static class MenuEditEmployee
{
    public static void EditEmployee()
    {
        string? answer;
        do
        {
            do
            {
                Console.WriteLine("Choose the filter of the employee:");
                Console.WriteLine("\t1 - Last name\n\t2 - ID\n\t3 - Back to menu");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3");

            switch (answer)
            {
                case "1":
                    Console.WriteLine("Last name: ");
                    
                    EmployeeDataChangeByLastName.EmployeeChangeLastNameData();
                    
                    break;
                case "2":
                    Console.WriteLine("ID: ");

                    EmployeeDataChangeByCode.EmployeeChangeCodeData();

                    break;
                case "3":
                    break;
            }
        } while (answer != "3");
    }
}