using System.Net.Mime;
using Drive.Data.Entities.Models;
using Drive.Presentation.Helpers;
using Drive.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Drive.Presentation.Actions.DriveMenu.MyDisk;

public class EditFileAction
{
    public static void EditFile(string fileName, Guid? folderId, string mail)
    {   
        
        var oldText = FileEditingRepository.GetFileText(fileName, folderId);
        var line = "";
        var isValid1 = false;
        var exit = false;
        
        while (!exit)
        {
            var key = Reader.TryReadFileChar();
            isValid1 = FileEditingRepository.EnterPressed(key, line, fileName, folderId);
            line = FileEditingRepository.BackSpacePressed(key, line);
            
            if (isValid1)
            {
                line = "";
            }

            else
            {
                
                line = FileEditingRepository.AddKeyInText(key, line);
                var (isValid2,command) = FileEditingRepository.DoesStringContainCommand(line);

                if (isValid2)
                {
                    exit = Command(command, fileName, folderId, mail);
                }
                
                Console.Clear();
                Console.WriteLine(line);
            }
        }
    }
    
    
    public static bool Command(string currentLine, string fileName, Guid? folderId, string mail)
    {
        switch (currentLine)
        {
            case "help":
                Writer.EditingHelpCommand();
                return false;
            case "spremanje i izlaz":
                FileEditingRepository.ExitAndSave(fileName,folderId);
                Console.WriteLine("Datoteka je uređena");
                MyDiskAction.MyDisk(mail, folderId);
                return true;
            case "izlaz bez spremanja":
                FileEditingRepository.ExitAndDontSave();
                Console.WriteLine("Datoteka nije uređena");
                MyDiskAction.MyDisk(mail, folderId);
                return true;
        }
        return false;
    }
    
}