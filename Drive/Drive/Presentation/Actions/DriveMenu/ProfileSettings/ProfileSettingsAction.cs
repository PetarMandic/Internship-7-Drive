using Drive.Presentation.Helpers;
using Drive.Presentation.Actions;
using Drive.Domain.Repositories;
using Drive.Presenation.Actions.Register;

namespace Drive.Presentation.Actions.DriveMenu.ProfileSettings;

public class ProfileSettingsAction
{
    public static void ProfileSettingsMail(string mail)
    {
        var newMail = UserRegisterAction.InputMail();
        
        UserRegisterAction.RepeatString();
        
        ProfileSettingsRepository.ChangeMail(mail, newMail);
        
        Console.WriteLine("Uspiješno promijenjen mail");
        Thread.Sleep(2000);
        DriveMenuDisplay.DriveMenu(newMail);
        
    }

    public static void ProfileSettingsPassword(string mail)
    {
        Writer.WritePassword();
        var newPassword = Reader.TryReadPassword();
        var password = ProfileSettingsRepository.FindUserPassword(mail);
        
        UserRegisterAction.RepeatString();
        
        ProfileSettingsRepository.ChangePassword(password, mail, newPassword);
        
        Console.WriteLine("Uspiješno promijenjena lozinka");
        Thread.Sleep(2000);
        DriveMenuDisplay.DriveMenu(newPassword);
    }
    
    
    
}