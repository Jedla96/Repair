using Repair.Menu.DatabaseMethods;
using Repair.Menu.DatabaseMethods.UserAdd;

namespace Repair.Menu;

public static class MenuNewUser
{
    public static void NewUser(List<User> users)
    {
        string code = IdGenerator.GenerateId();
        Console.WriteLine("Here is the ID of a new user: " + code + "\nFill the following:\n ");

        users.Add(new User(string.Empty, string.Empty, DateTime.MinValue, string.Empty,
            code));
        int index = users.FindIndex(e => e.Code == code);
        if (index >= 0)
        {
            UserDataChoose.FirstNameChoose(users, index);
            UserDataChoose.LastNameChoose(users, index);
            UserDataChoose.DateChoose(users, index);
            UserDataChoose.PositionChoose(users, index);
            UserStoreToDb.StoreUserToDatabase(users,index);
        }
        Console.WriteLine(
            "New user was successfully added. Do you wanna see list of all users ?\n1: Yes\n2: No");
        string? listAnswer = Console.ReadLine();
        switch (listAnswer)
        {
            case "1":
                MenuListOfUsers.ListOfUsers();
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }
    }
}