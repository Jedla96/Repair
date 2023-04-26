
namespace Repair.Menu.MenuUser;

public abstract class Menu
{
    public static void MenuSetUp(string? code)
    {
        string? answer;
        do
        {
            do
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1 - See bank account info\n2 - Send money\n3 - Exit");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2" && answer != "3");

            switch (answer)
            {
                case "1":
                    AccInfo.BankAccountInfo(code);
                    break;
                case "2":
                    MoneyTransfer.SendMoney(code);
                    break;
                case "3":
                    break;
                default:
                    MenuSetUp(code);
                    break;
            }
        } while (answer != "3");
    }
}