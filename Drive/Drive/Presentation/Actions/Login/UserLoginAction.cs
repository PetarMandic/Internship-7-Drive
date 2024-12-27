using Drive.Domain.Repositories;
using Drive.Presenation.Factories;
using Drive.Presenation.Helpers;
using Drive.Presentation.Actions.DriveMenu;

namespace Drive.Presenation.Actions.Login;

public class UserLoginAction
{
    public static void UserLogin()
    {
        var firstCheck = InputMail();
        var secondCheck = InputPassword();

        while (!firstCheck.Item1 || !secondCheck.Item1)
        {
            LoginTimeout();
            firstCheck = InputMail();
            secondCheck = InputPassword();
        }
        
        Console.WriteLine("Uspiješna prijava !!!");
        DriveMenuDisplay.DriveMenu(firstCheck.Item2, secondCheck.Item2);
    }

    public static (bool,string) InputMail()
    {
        Writer.WriteMail();
        var mail = Reader.TryReadMail();
        var isValid = LoginRepository.MailExist(mail);
        
        return (isValid,mail);
    }

    public static (bool, string) InputPassword()
    {
        Writer.WritePassword();
        var password = Reader.TryReadPassword();
        var isValid = LoginRepository.PasswordMatch(password);
        
        return (isValid,password);
    }

    public static void LoginTimeout()
    {
        TimeSpan remainingTime = LoginRepository.TimePassed();
        while (remainingTime.TotalSeconds != 0)
        {
            remainingTime = LoginRepository.TimePassed();
            Console.Clear();
            Console.WriteLine($"Morate čekati još {remainingTime.TotalSeconds:F0} sekundi pre nego što pokušate ponovo.");
            Thread.Sleep(1000);
        }
    }
    
    
}