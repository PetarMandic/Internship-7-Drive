using Drive.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Domain.Repositories;

public class FileEditingRepository
{
    public static bool EnterPressed(ConsoleKeyInfo key, string currentLine, string fileName, Guid? folderId)
    {
        if (key.Key == ConsoleKey.Enter)
        {
            Lists.newText.Add(currentLine);
            return true;
        }

        return false;
    }

    public static string BackSpacePressed(ConsoleKeyInfo key, string currentLine)
    {
        if (key.Key == ConsoleKey.Backspace)
        {
            if (currentLine.Length > 0)
            {
                currentLine = currentLine.Substring(0, currentLine.Length - 1);
                return currentLine;
            }
            else
            {
                currentLine = Lists.newText.Last(); 
                Lists.newText.RemoveAt(Lists.newText.Count - 1);
                return currentLine;
            }
        }

        return currentLine;
    }

    public static string AddKeyInText(ConsoleKeyInfo key, string currentLine)
    {
        if (key.Key != ConsoleKey.Backspace)
        {
            currentLine += key.KeyChar;
            return currentLine;
        }
        return currentLine;
    }

    public static List<string> GetFileText(string fileName, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var file = context.Files.FirstOrDefault(f => f.Name == fileName && folderId == f.FolderId);
            return file.Text;
        }
    }

    public static (bool, string) DoesStringContainCommand(string currentLine)
    {
        if (currentLine.Contains("(:)help") || currentLine.Contains("(:)spremanje i izlaz") || currentLine.Contains("(:)izlaz bez spremanja"))
        {
            var command = currentLine switch
            {
                var line when line.Contains("(:)help") => "help",
                var line when line.Contains("(:)spremanje i izlaz") => "spremanje i izlaz",
                var line when line.Contains("(:)izlaz bez spremanja") => "izlaz bez spremanja",
                _ => null 
            };

            return (true, command);
        }

        return (false, null);
    }


    public static void ExitAndSave(string fileName, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var file = context.Files.FirstOrDefault(f => f.Name == fileName && folderId == f.FolderId);
            foreach (var textLine in Lists.newText)
            {
                file.Text.Add(textLine); 
                file.EditingTime = DateTime.UtcNow;
            }
            context.SaveChanges();
            Lists.newText.Clear();
        }
    }

    public static void ExitAndDontSave()
    {
        Lists.newText.Clear();
    }

}