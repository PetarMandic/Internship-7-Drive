using Drive.Domain.Repositories;
using Drive.Presenation.Factories;
using Drive.Presenation.Actions.Login;
using Drive.Presenation.Actions.Register;
using Drive.Presentation.Actions.DriveMenu.MyDisk;
using Drive.Presentation.Actions.DriveMenu.ProfileSettings;
using Drive.Presentation.Actions.DriveMenu.Sharing;

namespace Drive.Presenation.Extensions;

public static class ActionExtensions
{
    public static void PrintActionsAndOpen(this List<string> menuItems, string mail)
    {
        var exit = false;
        
        while (!exit)
        {
            PrintActions(menuItems, NavigationRepository.currentSelection);
            
            var key = Console.ReadKey(true).Key;

            var pair = NavigationRepository.KeyPressed(key, menuItems);
            
            if (pair.Item1)
            {
                exit = OpenAction(pair.Item2, menuItems, mail);
            }
            
        }
        
    }

    private static void PrintActions(this List<string> menuItems, int currentSelection)
    {
        Console.Clear();
        
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == currentSelection)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("> " + menuItems[i]);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  " + menuItems[i]);
            }
        }
    }

    private static bool OpenAction(int currentSelection, List<string> menuItems, string mail)
    {
        if (menuItems.Count == 2)
        {
            ProfileSettingsMenuOptions(currentSelection, mail);
        }
        else if (menuItems.Count == 3)
        {
            StartMenuOptions(currentSelection);
        }

        else
        {
            DriveMenuOptions(currentSelection, mail);
        }
        return true;
    }

    public static void StartMenuOptions(int currentSelection)
    {
        switch (currentSelection)
        {
            case 0:
                Console.Clear();
                UserLoginAction.UserLogin();
                break;
            case 1:
                Console.Clear();
                UserRegisterAction.UserRegister();
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("IZAŠLI STE IZ APLIKACIJE");
                break;
        }
    }

    public static void DriveMenuOptions(int currentSelection, string mail)
    {
        switch (currentSelection)
        {
            case 0:
                Console.Clear();
                MyDiskAction.MyDisk(mail, null);
                break;
            case 1:
                Console.Clear();
                SharingAction.Sharing(mail, null);
                break;
            case 2:
                Console.Clear();
                ProfileSettingsDisplay.ProfileSettingsMenu(mail);
                break;
            case 3:
                Console.Clear();
                var mainMenuActions = MainMenuFactory.CreateActions();
                PrintActionsAndOpen(mainMenuActions, "");
                break;
        }
    }

    public static void ProfileSettingsMenuOptions(int currentSelection, string mail)
    {
        switch (currentSelection)
        {
            case 0:
                Console.Clear();
                ProfileSettingsAction.ProfileSettingsMail(mail);
                break;
            case 1:
                Console.Clear();
                ProfileSettingsAction.ProfileSettingsPassword(mail);
                break;
        }
    }
}