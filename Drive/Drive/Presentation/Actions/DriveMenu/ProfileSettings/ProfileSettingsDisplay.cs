using Drive.Presenation.Factories;
using Drive.Presenation.Extensions;

namespace Drive.Presentation.Actions.DriveMenu.ProfileSettings;

public class ProfileSettingsDisplay
{
    public static void ProfileSettingsMenu(string mail)
    {
        var driveMenuAction = ProfileSettingsMenuFactory.ChangeProfileActions();
        ActionExtensions.PrintActionsAndOpen(driveMenuAction, mail);
    }
}