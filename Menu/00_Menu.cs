
using Repair.Menu.DatabaseMethods.UserAdd;

namespace Repair.Menu;

public static class Menu
{
    public static void MenuSetUp(List<User> users)
    {
        string ?answer;
        do
        {
            do
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1 - Add new user\n2 - See all users\n3 - Edit user data\n4 - Delete user\n5 - Exit");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3" && answer != "4" && answer != "5");

            switch (answer)
            {
                case "1":
                    MenuNewUser.NewUser(users);
                    break;
                case "2":
                    MenuListOfUsers.ListOfUsers();
                    break;
                case "3":
                    MenuEditUser.EditUser();
                    break;
                case "4":
                    MenuDeleteUser.DeleteUser();
                    break;
                case "5":
                    break;
                default:
                    MenuSetUp(users);
                    break;
            }
        } while (answer != "5");
    }
}