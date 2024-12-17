using Drive.Domain.Repositories;

namespace Drive.Presenation.Extensions;

public static class ActionExtensions
{
    public static void PrintActionsAndOpen(List<string> menuItems)
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

    private static void PrintActions(List<string> menuItems, int currentSelection)
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
        
        return true;
    }
}