using Drive.Domain.Repositories;
using Drive.Presenation.Actions;
using Drive.Presenation.Actions.Login;
using Drive.Presenation.Actions.Register;

namespace Drive.Presenation.Extensions;

public static class ActionExtensions
{
    public static void PrintActionsAndOpen(this List<string> menuItems)
    {
        var exit = false;
        
        while (!exit)
        {
            PrintActions(menuItems, NavigationRepository.currentSelection);
            
            var key = Console.ReadKey(true).Key;

            var pair = NavigationRepository.KeyPressed(key, menuItems);
            
            if (pair.Item1)
            {
                exit = OpenAction(pair.Item2);
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

    private static bool OpenAction(int currentSelection)
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
        return true;
    }
}