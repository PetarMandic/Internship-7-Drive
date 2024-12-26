using Drive.Domain.Repositories;
using Drive.Presenation.Helpers;
namespace Drive.Presenation.Actions.Register;

public class UserRegisterAction
{
    public static void UserRegister()
    {
        InputMail();
        InputPassword();
    }

    public static void InputMail()
    {
        Writer.WriteMail();
        var mail = Reader.TryReadMail();

        var pair = RegisterRepository.MailFormatValid(mail);

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
    }

    public static void InputPassword()
    {
        Writer.WritePassword();
        var password = Reader.TryReadPassword();
        
        Console.WriteLine("Potvrdite lozinku: ");
        var confirmPassword = Reader.TryReadPassword();
        
        var isValid = RegisterRepository.PasswordConfirmation(password, confirmPassword);

        if (!isValid)
        {
            Console.WriteLine("Niste potvrdili lozinku");
        }
    }
}