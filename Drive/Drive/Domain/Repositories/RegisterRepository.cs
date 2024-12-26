using System.Text.RegularExpressions;

namespace Drive.Domain.Repositories;

public class RegisterRepository
{
    public static (bool,bool) MailFormatValid(string mail)
    {
        var isValid1 = CountMailChar(mail);
        var isValid2 = Regex.IsMatch(mail, @"^[^@]+@[^@]+\.[^@]{3,}$");
        return (isValid1, isValid2);
    }

    public static bool CountMailChar(string mail)
    {
        var count = 0;
        
        foreach (var symbol in mail)
        {
            if (symbol == '@')
            {
                count++;
            }
        }

        switch (count)
        {
            case 1:
                return true;
            case >1:
                return false;
        }
        
        return true;
    }

    public static bool PasswordConfirmation(string password, string confirmPassword)
    {
        for (var i = 0; i < password.Length; i++)
        {
            if (password[i] != confirmPassword[i])
            {
                return false;
            }
        }
        
        return true;
    }
}          