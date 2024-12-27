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

    public static string RandomString()
    {
        Random random = new Random();
        
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string allCharacters = letters + digits;

        string randomString = string.Empty;
        
        bool hasLetter = false;
        bool hasDigit = false;

        while (randomString.Length < 6 || !hasLetter || !hasDigit)
        {
            randomString = string.Empty;
            hasLetter = false;
            hasDigit = false;
            
            for (int i = 0; i < 6; i++)
            {
                char randomChar = allCharacters[random.Next(allCharacters.Length)];
                randomString += randomChar;
                
                if (char.IsLetter(randomChar)) hasLetter = true;
                if (char.IsDigit(randomChar)) hasDigit = true;
            }
        }

        return randomString;
    }
    
    
}          