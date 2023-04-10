using Repair.Menu.DatabaseMethods.UserAdd;

namespace Repair
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User>users = new List<User>();
            Menu.Menu.MenuSetUp(users);
        }
    }
}
