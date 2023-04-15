using Repair.Menu.DatabaseMethods.UserEdit;

namespace Repair.Menu.MenuAdmin;

public static class MenuEditUser
{
    public static void EditUser()
    {
        string? answer;
        do
        {
            do
            {
                Console.WriteLine("Choose the filter of the user:");
                Console.WriteLine("\t1 - Last name\n\t2 - Code\n\t3 - ID\n\t4 - Back to menu");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3" && answer != "4");

            switch (answer)
            {
                case "1":
                    EditByLastName.EditUserDataByIndex();

                    break;
                case "2":
                    EditByCode.EditUserDataByIndex();

                    break;
                case "3":
                    EditById.EditUserDataByIndex();
                    break;
                case "4":
                    break;
            }
        } while (answer != "4");
    }
}