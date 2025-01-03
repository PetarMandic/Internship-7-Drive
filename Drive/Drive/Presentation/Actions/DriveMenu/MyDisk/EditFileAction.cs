using Drive.Presentation.Helpers;
using Drive.Domain.Repositories;

namespace Drive.Presentation.Actions.DriveMenu.MyDisk;

public class EditFileAction
{
    public static void EditFile(string fileName, Guid? folderId, string mail, Guid userId)
    {   
        
        var oldText = FileEditingRepository.GetFileText(fileName, folderId);
        for (int i = 0; i < oldText.Count; i++)
        {
            Console.WriteLine(oldText[i]);
        }
        var line = "";
        var isValid1 = false;
        var exit = false;
        
        while (!exit)
        {
            var key = Reader.TryReadFileChar();
            isValid1 = FileEditingRepository.EnterPressed(key, line);
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
                    var commandLength = command.Length;
                    
                    if (line.Length >= commandLength)
                    {
                        line = line.Remove(line.Length - commandLength, commandLength);
                    }
                    exit = Command(command, fileName, folderId, mail, userId);
                }
                Console.Clear();
                Console.WriteLine(line);
            }
        }
    }
    
    
    public static bool Command(string currentLine, string fileName, Guid? folderId, string mail, Guid userId)
    {
        switch (currentLine)
        {
            case "(:)help":
                Writer.EditingHelpCommand();
                Thread.Sleep(2000);
                return false;
            case "(:)spremanje i izlaz":
                FileEditingRepository.ExitAndSave(fileName,folderId);
                Console.WriteLine("Datoteka je uređena");
                Thread.Sleep(2000);
                MyDiskAction.MyDisk(mail, folderId);
                return true;
            case "(:)izlaz bez spremanja":
                FileEditingRepository.ExitAndDontSave();
                Console.WriteLine("Datoteka nije uređena");
                Thread.Sleep(2000);
                MyDiskAction.MyDisk(mail, folderId);
                return true;
            case "(:)podijeli datoteku":
                var userMail = Reader.TryReadMail();
                var emailExist = UserRepository.CheckIfUserExists(userMail);
                var usersId = MyDiskRepository.FindUserId(userMail);
                MyDiskRepository.ShareFile(fileName, folderId, usersId, userId);
                Thread.Sleep(2000);
                break;
            case "(:)prestani dijeliti datoteku":
                userMail = Reader.TryReadMail();
                emailExist = UserRepository.CheckIfUserExists(userMail);
                usersId = MyDiskRepository.FindUserId(userMail);
                MyDiskRepository.StopShareFile(fileName, folderId, usersId);
                Thread.Sleep(2000);
                break;
            case "(:)otvori komentare":
                Console.Clear();
                CommentAction.Comment(fileName, folderId, mail);
                break;
        }
        return false;
    }
    
}