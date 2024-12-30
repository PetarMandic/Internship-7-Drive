using Drive.Presenation.Extensions;
using Drive.Presenation.Factories;

namespace Drive.Presentation.Actions.DriveMenu;

public class DriveMenuDisplay
{
    public static void DriveMenu(string mail)
    {
        var driveMenuActions = DriveMenuFactory.CreateAction();
        ActionExtensions.PrintActionsAndOpen(driveMenuActions, mail);
    }
}
