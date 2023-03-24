using Repair.Menu.DatabaseMethods.EmployeeEdit;

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
                Console.WriteLine("\t1 - Last name\n\t2 - Code\n\t3 - ID\n\t4 - Back to menu");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3" && answer != "4");

            switch (answer)
            {
                case "1":
                    Console.WriteLine("Last name: ");

                    EditByLastName.EditEmployeeDataByIndex();

                    break;
                case "2":
                    Console.WriteLine("Code: ");

                    EditByCode.EditEmployeeDataByIndex();

                    break;
                case "3":
                    Console.WriteLine("ID: ");
                    EditById.EditEmployeeDataByIndex();
                    break;
                case "4":
                    break;
            }
        } while (answer != "4");
    }
}