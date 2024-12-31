using Drive.Presentation.Helpers;
using Drive.Domain.Repositories;

namespace Drive.Presentation.Actions.DriveMenu.MyDisk;

public class MyDiskAction
{
    public static void MyDisk(string mail, Guid? parentFolderId)
    {
        var userId = MyDiskRepository.FindUserId(mail);
        
        if (parentFolderId == null)
        {
            FirstDisplay(userId);
        }
        else
        {
            AnyOtherDisplay(userId, parentFolderId);
        }
        
        Console.WriteLine("Unesite komandu: ");
        var command = Reader.TryReadCommand();

        Commands(command, parentFolderId, mail, userId);
    }

    public static void FirstDisplay(Guid userId)
    {
        var folders = MyDiskRepository.FindFolders(userId, null);
        var files = MyDiskRepository.FindFiles(userId, null);
        Writer.PrintFoldersAndFiles(folders, files);
    }

    public static void AnyOtherDisplay(Guid userId, Guid? parentFolderId)
    {
        var folders = MyDiskRepository.FindFolders(userId, parentFolderId);
        var files = MyDiskRepository.FindFiles(userId, parentFolderId);
        Writer.PrintFoldersAndFiles(folders, files);
    }
    
    public static void Commands(string command, Guid? parentFolderId,string mail, Guid userId)
    {
        var commandLine = command;
        command = MyDiskRepository.ReturnCommand(command);
        switch(command)
        {
            case "help":
                Console.Clear();
                Writer.HelpCommand();
                MyDisk(mail, parentFolderId);
                break;
            case "stvori mapu":
                Console.Clear();
                var nameOfFolder = MyDiskRepository.ReturnName(commandLine);
                MyDiskRepository.CreateFolder(nameOfFolder, parentFolderId, userId);
                Console.WriteLine("Mapa je stvorena");
                Thread.Sleep(2000);
                Console.Clear();
                MyDisk(mail, parentFolderId);
                break;
            case "stvori datoteku":
                Console.Clear();
                var nameOfFile = MyDiskRepository.ReturnName(commandLine);
                MyDiskRepository.CreateFile(nameOfFile, parentFolderId);
                Console.WriteLine("Datoteka je stvorena");
                Thread.Sleep(2000);
                MyDisk(mail, parentFolderId);
                break;
            case "uđi":
                Console.Clear();
                nameOfFolder = CheckNameFolder(commandLine, parentFolderId,mail);
                var parentId = MyDiskRepository.OpenFolder(nameOfFolder, parentFolderId);
                MyDisk(mail, parentId);
                break;
            case "uredi":
                Console.Clear();
                nameOfFile = CheckNameFile(commandLine, parentFolderId, mail);
                EditFileAction.EditFile(nameOfFile, parentFolderId, mail);
                break;
            case "izbriši mapu":
                Console.Clear();
                nameOfFolder = CheckNameFolder(commandLine, parentFolderId,mail);
                MyDiskRepository.DeleteFolder(nameOfFolder, parentFolderId);
                Console.WriteLine("Mapa je izbrisana");
                Thread.Sleep(2000);
                MyDisk(mail,parentFolderId);
                break;
            case "izbriši datoteku":
                Console.Clear();
                nameOfFile = CheckNameFile(commandLine, parentFolderId, mail);
                MyDiskRepository.DeleteFile(nameOfFile, parentFolderId);
                Console.WriteLine("Datoteka je izbrisana");
                Thread.Sleep(2000);
                MyDisk(mail,parentFolderId);
                break;
            case "promjeni naziv mape":
                Console.Clear();
                nameOfFolder = CheckNameFolder(commandLine, parentFolderId,mail);
                var newName = MyDiskRepository.ReturnNewName(commandLine);
                MyDiskRepository.RenameFolder(nameOfFolder, newName, parentFolderId);
                Console.WriteLine("Promjenjen je naziv mape");
                Thread.Sleep(2000);
                MyDisk(mail,parentFolderId);
                break;
            case "promjeni naziv datoteke":
                Console.Clear();
                nameOfFile = CheckNameFile(commandLine, parentFolderId, mail);
                newName = MyDiskRepository.ReturnNewName(commandLine);
                MyDiskRepository.RenameFile(nameOfFile, newName, parentFolderId);
                Console.WriteLine("Promjenjen je naziv datoteke");
                Thread.Sleep(2000);
                MyDisk(mail,parentFolderId);
                break;
            case "povratak":
                Console.Clear();
                DriveMenuDisplay.DriveMenu(mail);
                MyDisk(mail,parentFolderId);
                break;
        }

    }

    public static string CheckNameFolder(string commandLine, Guid? parentFolderId,string mail)
    {
        var nameOfFolder = MyDiskRepository.ReturnName(commandLine);
        var nameExist = MyDiskRepository.FolderExists(nameOfFolder, parentFolderId);
        Writer.DoesntExist(nameExist, "mape", mail, parentFolderId);
        return nameOfFolder;
    }

    public static string CheckNameFile(string commandLine, Guid? parentFolderId, string mail)
    {
        var nameOfFile = MyDiskRepository.ReturnName(commandLine);
        var nameExist = MyDiskRepository.FileExists(nameOfFile, parentFolderId);
        Writer.DoesntExist(nameExist, "datoteke", mail, parentFolderId);
        return nameOfFile;
    }
}