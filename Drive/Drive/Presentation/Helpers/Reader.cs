namespace Drive.Presenation.Helpers;

public class Reader
{
    public static string TryReadMail()
    {
        var email = Console.ReadLine();
        return email;
    }

    public static string TryReadPassword()
    {
        var password = Console.ReadLine();
        return password;
    }
}