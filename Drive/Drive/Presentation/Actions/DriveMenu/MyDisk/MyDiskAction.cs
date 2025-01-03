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

        var commandValid = Commands(command, parentFolderId, mail, userId);

        if (!commandValid)
        {
            Console.WriteLine("Niste unijeli ispravno komandu");
            MyDisk(mail, parentFolderId);
        }
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
    
    public static bool Commands(string command, Guid? parentFolderId,string mail, Guid userId)
    {
        var commandLine = command;
        command = MyDiskRepository.ReturnCommand(command);
        switch(command)
        {
            case "help":
                Help(parentFolderId, mail);
                break;
            case "stvori mapu":
                CreateFolder(commandLine, parentFolderId, mail, userId);
                break;
            case "stvori datoteku":
                CreateFile(commandLine, parentFolderId, mail, userId);
                break;
            case "uđi":
                Open(commandLine, parentFolderId, mail, userId);
                break;
            case "uredi":
                Edit(commandLine, parentFolderId, mail, userId);
                break;
            case "izbriši mapu":
                DeleteFolder(commandLine, parentFolderId, mail);
                break;
            case "izbriši datoteku":
                DeleteFile(commandLine, parentFolderId, mail);
                break;
            case "promjeni naziv mape":
                RenameFolder(commandLine, parentFolderId, mail, userId);
                break;
            case "promjeni naziv datoteke":
                RenameFile(commandLine, parentFolderId, mail, userId);
                break;
            case "podjeli mapu s":
                ShareFolder(commandLine, parentFolderId, mail, userId);
                break;
            case "prestani dijeliti mapu s":
                StopShare(commandLine, parentFolderId, mail);
                break;
            case "povratak":
                Console.Clear();
                DriveMenuDisplay.DriveMenu(mail);
                MyDisk(mail,parentFolderId);
                break;
        }

        return false;
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

    public static void Help(Guid? parentFolderId,string mail)
    {
        Console.Clear();
        Writer.HelpCommand();
        MyDisk(mail, parentFolderId);
    }

    public static void CreateFolder(string commandLine, Guid? parentFolderId,string mail, Guid userId)
    {
        Console.Clear();
        var nameOfFolder = MyDiskRepository.ReturnName(commandLine);
        MyDiskRepository.CreateFolder(nameOfFolder, parentFolderId, userId);
        Console.WriteLine("Mapa je stvorena");
        Thread.Sleep(2000);
        Console.Clear();
        MyDisk(mail, parentFolderId);
    }

    public static void CreateFile(string commandLine, Guid? parentFolderId,string mail, Guid userId)
    {
        Console.Clear();
        var nameOfFile = MyDiskRepository.ReturnName(commandLine);
        MyDiskRepository.CreateFile(nameOfFile, parentFolderId, userId);
        Console.WriteLine("Datoteka je stvorena");
        Thread.Sleep(2000);
        MyDisk(mail, parentFolderId);
    }

    public static void Open(string commandLine, Guid? parentFolderId,string mail, Guid userId)
    {
        Console.Clear();
        var nameOfFolder = CheckNameFolder(commandLine, parentFolderId,mail);
        var parentId = MyDiskRepository.OpenFolder(nameOfFolder, parentFolderId, userId);
        MyDisk(mail, parentId);
    }

    public static void Edit(string commandLine, Guid? parentFolderId,string mail, Guid userId)
    {
        Console.Clear();
        var nameOfFile = CheckNameFile(commandLine, parentFolderId, mail);
        EditFileAction.EditFile(nameOfFile, parentFolderId, mail, userId);
    }

    public static void DeleteFolder(string commandLine, Guid? parentFolderId,string mail)
    {
        Console.Clear();
        var nameOfFolder = CheckNameFolder(commandLine, parentFolderId,mail);
        MyDiskRepository.DeleteFolder(nameOfFolder, parentFolderId);
        Console.WriteLine("Mapa je izbrisana");
        Thread.Sleep(2000);
        MyDisk(mail,parentFolderId);
    }

    public static void DeleteFile(string commandLine, Guid? parentFolderId,string mail)
    {
        Console.Clear();
        var nameOfFile = CheckNameFile(commandLine, parentFolderId, mail);
        MyDiskRepository.DeleteFile(nameOfFile, parentFolderId);
        Console.WriteLine("Datoteka je izbrisana");
        Thread.Sleep(2000);
        MyDisk(mail,parentFolderId);
    }

    public static void RenameFolder(string commandLine, Guid? parentFolderId,string mail, Guid userId)
    {
        Console.Clear();
        var nameOfFolder = CheckNameFolder(commandLine, parentFolderId, mail);
        var newName = MyDiskRepository.ReturnNewName(commandLine);
        MyDiskRepository.RenameFolder(nameOfFolder, newName, parentFolderId, userId);
        Console.WriteLine("Promjenjen je naziv mape");
        Thread.Sleep(2000);
        MyDisk(mail,parentFolderId);
    }

    public static void RenameFile(string commandLine, Guid? parentFolderId,string mail, Guid userId)
    {
        Console.Clear();
        var nameOfFile = CheckNameFile(commandLine, parentFolderId, mail);
        var newName = MyDiskRepository.ReturnNewName(commandLine);
        MyDiskRepository.RenameFile(nameOfFile, newName, parentFolderId, userId);
        Console.WriteLine("Promjenjen je naziv datoteke");
        Thread.Sleep(2000);
        MyDisk(mail,parentFolderId);
    }

    public static void ShareFolder(string commandLine, Guid? parentFolderId, string mail, Guid userId)
    {
        Console.Clear();
        var userMail = MyDiskRepository.ReturnName(commandLine);
        var emailExist = UserRepository.CheckIfUserExists(userMail);
        if (!emailExist)
        {
            Console.WriteLine("Mail ne postoji");
            Thread.Sleep(2000);
            MyDisk(mail,parentFolderId);
        }
        
        var usersId = MyDiskRepository.FindUserId(userMail);
        MyDiskRepository.ShareFolder(parentFolderId, usersId, userId);
        Console.WriteLine("Mapa je uspiješno podijeljena");
        Thread.Sleep(2000);
        MyDisk(mail,parentFolderId);
    }

    public static void StopShare(string commandLine, Guid? parentFolderId,string mail)
    {
        Console.Clear();
        var userMail = Reader.TryReadMail();
        var usersId = MyDiskRepository.FindUserId(userMail);
        var emailExist = UserRepository.CheckIfUserExists(userMail);
        
        if (!emailExist)
        {
            Console.WriteLine("Mail ne postoji");
            Thread.Sleep(2000);
            MyDisk(mail,parentFolderId);
        }
        
        var nameOfFolder = MyDiskRepository.ReturnName(commandLine);
        MyDiskRepository.StopShare(nameOfFolder, parentFolderId, usersId);
        Console.WriteLine("Mapa više nije podijeljena korisniku");
        Thread.Sleep(2000);
        MyDisk(mail,parentFolderId);
    }
}