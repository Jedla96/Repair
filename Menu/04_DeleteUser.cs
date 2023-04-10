using Repair.Menu.DatabaseMethods.UserDelete;

namespace Repair.Menu;

public static class MenuDeleteUser
{
    public static void DeleteUser()
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
                    DeleteByLastName.DeleteUserByLastName();

                    break;
                case "2":
                    DeleteByCode.DeleteUserByCode();

                    break;
                case "3":
                    DeleteById.DeleteUserById();
                    break;
                case "4":
                    break;
            }
        } while (answer != "4");
    }
}