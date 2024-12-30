namespace Drive.Presentation.Helpers;

public class Reader
{
    public static string TryReadMail()
    {
        var email = Console.ReadLine();
        while (email == String.Empty)
        {
            email = Console.ReadLine();
        }
        return email;
    }

    public static string TryReadPassword()
    {
        var password = Console.ReadLine();
        while (password == string.Empty)
        {
            password = Console.ReadLine();
        }
        return password;
    }

    public static string TryReadCommand()
    {
        var command = Console.ReadLine();
        while(command == String.Empty)
        {
            command = Console.ReadLine();
        }
        return command;
    }

    public static ConsoleKeyInfo TryReadFileChar()
    {
        var key = Console.ReadKey();
        return key;
    }
}