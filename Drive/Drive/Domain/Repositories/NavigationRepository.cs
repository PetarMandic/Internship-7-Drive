using Drive.Data;
namespace Drive.Domain.Repositories
{
    public class NavigationRepository
    {
        public static int currentSelection = 0;
        
        public static (bool,int) KeyPressed(ConsoleKey key, List<string> menuItems)
        {
            var enter = false;
            
            if (key == ConsoleKey.UpArrow)  
            {
                currentSelection--;
                if (currentSelection < 0)
                    currentSelection = menuItems.Count - 1; 
            }
            else if (key == ConsoleKey.DownArrow)  
            {
                currentSelection++;
                if (currentSelection >= menuItems.Count)
                    currentSelection = 0;  
            }
            else if (key == ConsoleKey.Enter)
            {
                enter = true;
            }
            return (enter, currentSelection);
        }
        
    }
}
