using Drive.Domain.Repositories;
using Drive.Presentation.Helpers;
using Drive.Presenation.Factories;
using Drive.Presenation.Extensions;

namespace Drive.Presenation.Actions.Register;

public class UserRegisterAction
{
    public static void UserRegister()
    {
        var mail = InputMail();
        var password = InputPassword();
        RepeatString();
        UserRepository.AddUser(mail, password);
        
        Console.WriteLine("Uspiješna registracija !!!");
        Thread.Sleep(3000);
        
        Console.Clear();
        var mainMenuActions = MainMenuFactory.CreateActions();
        ActionExtensions.PrintActionsAndOpen(mainMenuActions, "");
    }

    public static string InputMail()
    {
        
        Writer.WriteMail();
        var mail = Reader.TryReadMail();
        var pair = RegisterRepository.MailFormatValid(mail);
        var mailExist = RegisterRepository.MailExist(mail);
        if (!pair.Item1)
        {
            Console.Clear();
            Console.WriteLine("Mail ne može sadržavati više '@' znakova");
            InputMail();
        }
        else if (!pair.Item2)
        {
            Console.Clear();
            Console.WriteLine("Format maila nije ispunjen([string min 1 char]@[string min 2 chara].[string min 3 chara])");
            InputMail();
        }
        else if (mailExist)
        {
            Console.Clear();
            Console.WriteLine("Mail već postoji");
            InputMail();
        }
        return mail;
    }

    public static string InputPassword()
    {
        Writer.WritePassword();
        var password = Reader.TryReadPassword();
        
        Console.WriteLine("Potvrdite lozinku: ");
        var confirmPassword = Reader.TryReadPassword();
        
        var isValid = RegisterRepository.PasswordConfirmation(password, confirmPassword);

        while(!isValid)
        {
            Console.WriteLine("Niste potvrdili lozinku");
            confirmPassword = Reader.TryReadPassword();
            isValid = RegisterRepository.PasswordConfirmation(password, confirmPassword);
        }
        return password;
    }

    public static void RepeatString()
    {
        var randomString = RegisterRepository.RandomString();
        Console.WriteLine("Prepišite ispisani string: " +randomString);
        
        var input = Console.ReadLine();
        while (input != randomString)
        {
            randomString = RegisterRepository.RandomString();
            Console.WriteLine("Prepišite ispisani string: " +randomString);
            input = Console.ReadLine();
        }
    }
}