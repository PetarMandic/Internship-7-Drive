using Drive.Domain.Repositories;
using Drive.Presentation.Actions.DriveMenu.MyDisk;
using Drive.Presentation.Helpers;

namespace Drive.Presentation.Actions.DriveMenu.Sharing;

public class SharingAction
{
    public static void Sharing(string mail,Guid? parentFolderId)
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
        var folders = SharingRepository.FindFolders(userId, null);
        var files = SharingRepository.FindFiles(userId, null);
        Writer.PrintFoldersAndFiles(folders, files);
    }

    public static void AnyOtherDisplay(Guid userId, Guid? parentFolderId)
    {
        var folders = SharingRepository.FindFolders(userId, parentFolderId);
        var files = SharingRepository.FindFiles(userId, parentFolderId);
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
                Writer.SharedHelpCommand();
                Sharing(mail,parentFolderId);
                break;
            case "stvori mapu":
                Console.Clear();
                var nameOfFolder = MyDiskRepository.ReturnName(commandLine);
                var usersId = SharingRepository.FindOwner(parentFolderId);
                SharingRepository.CreateFolder(nameOfFolder, parentFolderId, userId, usersId);
                Console.WriteLine("Mapa je stvorena");
                Thread.Sleep(2000);
                Console.Clear();
                Sharing(mail,parentFolderId);
                break;
            case "stvori datoteku":
                Console.Clear();
                var nameOfFile = MyDiskRepository.ReturnName(commandLine);
                usersId = SharingRepository.FindOwner(parentFolderId);
                SharingRepository.CreateFile(nameOfFile, parentFolderId, userId, usersId);
                Console.WriteLine("Datoteka je stvorena");
                Thread.Sleep(2000);
                Sharing(mail,parentFolderId);
                break;
            case "uđi":
                Console.Clear();
                nameOfFolder = MyDiskAction.CheckNameFolder(commandLine, parentFolderId, mail);
                parentFolderId = SharingRepository.OpenFolder(nameOfFolder, parentFolderId, userId);
                Sharing(mail,parentFolderId);
                break;
            case "uredi":
                Console.Clear();
                nameOfFile = MyDiskAction.CheckNameFile(commandLine, parentFolderId, mail);
                EditFileAction.EditFile(nameOfFile, parentFolderId, mail, userId);
                break;
            case "izbriši mapu":
                Console.Clear();
                nameOfFolder = MyDiskAction.CheckNameFolder(commandLine, parentFolderId,mail);
                SharingRepository.DeleteSharedFolder(nameOfFolder, parentFolderId, userId);
                Console.WriteLine("Mapa je izbrisana");
                Thread.Sleep(2000);
                Sharing(mail,parentFolderId);
                break;
            case "izbriši datoteku":
                Console.Clear();
                nameOfFile = MyDiskAction.CheckNameFile(commandLine, parentFolderId, mail);
                SharingRepository.DeleteSharedFile(nameOfFile, parentFolderId, userId);
                Console.WriteLine("Datoteka je izbrisana");
                Thread.Sleep(2000);
                Sharing(mail,parentFolderId);
                break;
            case "promjeni naziv mape":
                Console.Clear();
                nameOfFolder = MyDiskAction.CheckNameFolder(commandLine, parentFolderId,mail);
                var newName = MyDiskRepository.ReturnNewName(commandLine);
                MyDiskRepository.RenameFolder(nameOfFolder, newName, parentFolderId, userId);
                Console.WriteLine("Promjenjen je naziv mape");
                Thread.Sleep(2000);
                Sharing(mail,parentFolderId);
                break;
            case "promjeni naziv datoteke":
                Console.Clear();
                nameOfFile = MyDiskAction.CheckNameFile(commandLine, parentFolderId, mail);
                newName = MyDiskRepository.ReturnNewName(commandLine);
                MyDiskRepository.RenameFile(nameOfFile, newName, parentFolderId, userId);
                Console.WriteLine("Promjenjen je naziv datoteke");
                Thread.Sleep(2000);
                Sharing(mail,parentFolderId);
                break;
            case "povratak":
                Console.Clear();
                DriveMenuDisplay.DriveMenu(mail);
                Sharing(mail,parentFolderId);
                break;
        }

    }
}   
