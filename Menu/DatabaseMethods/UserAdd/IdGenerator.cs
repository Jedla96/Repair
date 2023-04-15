namespace Repair.Menu.DatabaseMethods.UserAdd;

static class IdGenerator
{
    private const string Chars = "0123456789";

    public static string GenerateId()
    {
        Random rnd = new Random();
        string code = "";
        for (int i = 0; i < 5; i++)
        {
            code += Chars[rnd.Next(Chars.Length)];
        }
        return code;
    }
}