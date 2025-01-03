using Drive.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Domain.Repositories;

public class MyDiskRepository
{
    public static bool FolderExists(string folderName, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.Name == folderName && f.ParentFolderId == folderId);
            if (folder == null)
            {
                return false;
            }
            return true;
        }
    }

    public static bool FileExists(string fileName, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var file = context.Files.FirstOrDefault(f => f.Name == fileName && f.FolderId == folderId);
            if (file == null)
            {
                return false;
            }
            return true;
        }
    }
    public static Guid FindUserId(string mail)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            User user = context.Users.FirstOrDefault(u => u.Mail == mail);
            Guid userId = user.Id;
            return userId;
        }
    }
    
    public static List<Folder> FindFolders(Guid userId, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folders = context.Folders
                .Where(f => f.ParentFolderId == folderId && f.UserId == userId) 
                .OrderBy(f => f.Name) 
                .ToList();
            return folders;
        }
    }

    public static List<File> FindFiles(Guid userId, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var files = context.Files
                .Where(f => f.FolderId == folderId && f.UserId == userId) 
                .OrderBy(f => f.EditingTime) 
                .ToList();
            return files;
        }
    }
    
    public static string ReturnName(string command)
    {
        var symbolCount = 0;
        var Symbol = "'";
        var name = "";
        for (var i = 0; i < command.Length; i++)
        {
            if (command[i] == Symbol[0] && symbolCount == 0)
            {       
                for (var j = i+1; j < command.Length - 1; j++)
                {
                    if (command[j] == Symbol[0])
                    {
                        return name;
                    }
                    name += command[j];
                }
            }
        }
        return name;
    }

    public static string ReturnNewName(string command)
    {
        var Symbol = "'";
        var name = "";
        var symbolCount = 0;
        for (var i = 0; i < command.Length; i++)
        {
            if (command[i] == Symbol[0])
            {
                symbolCount++;
            }

            else if (symbolCount == 3)
            {
                for (var j = i; j < command.Length - 1; j++)
                {
                    name += command[j];
                }
                return name;
            }
        }
        
        return name;
    }

    public static string ReturnCommand(string currentLine)
    {
        if (currentLine.Contains("help") || currentLine.Contains("stvori mapu") || currentLine.Contains("stvori datoteku") || 
            currentLine.Contains("uđi") || currentLine.Contains("uredi") || currentLine.Contains("izbriši mapu") || 
            currentLine.Contains("izbriši datoteku") || currentLine.Contains("promjeni naziv mape") || 
            currentLine.Contains("promjeni naziv datoteke") || currentLine.Contains("povratak") || 
            currentLine.Contains("podjeli mapu s") || currentLine.Contains("prestani dijeliti mapu s"))
        {
            var line = currentLine;  

            var command = line switch
            {
                var l when l.Contains("help") => "help",
                var l when l.Contains("stvori mapu") => "stvori mapu",
                var l when l.Contains("stvori datoteku") => "stvori datoteku",
                var l when l.Contains("uđi") => "uđi",
                var l when l.Contains("uredi") => "uredi",
                var l when l.Contains("izbriši mapu") => "izbriši mapu",
                var l when l.Contains("izbriši datoteku") => "izbriši datoteku",
                var l when l.Contains("promjeni naziv mape") => "promjeni naziv mape",
                var l when l.Contains("promjeni naziv datoteke") => "promjeni naziv datoteke",
                var l when l.Contains("povratak") => "povratak",
                var l when l.Contains("podjeli mapu s") => "podjeli mapu s",
                var l when l.Contains("prestani dijeliti mapu s") => "prestani dijeliti mapu s",
                _ => null
            };

            return command;
        }

        return null;
    }

    
    public static void CreateFolder(string nameOfFolder, Guid? parentFolderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var newFolder = new Folder(Guid.NewGuid(), nameOfFolder, parentFolderId, userId, null);
            context.Folders.Add(newFolder);
            context.SaveChanges();
        }
    }

    public static void CreateFile(string nameOfFile, Guid? parentFolderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var newFile = new File(Guid.NewGuid(), nameOfFile,DateTime.UtcNow , new List<string>(){""},parentFolderId, userId,null);
            context.Files.Add(newFile);
            context.SaveChanges();
        }
    }

    public static Guid? OpenFolder(string folderName, Guid? parentFolderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.Name == folderName && f.ParentFolderId == parentFolderId && f.UserId == userId);
            return folder.Id;
        }
    }

    public static void DeleteFolder(string folderName, Guid? parentFolderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.Name == folderName && parentFolderId == f.ParentFolderId);
            var folderToDelete = context.Folders
                .Include(f => f.SubFolders)
                .Include(f => f.Files) 
                .FirstOrDefault(f => f.Id == folder.Id);

            foreach (var subFolder in folderToDelete.SubFolders.ToList())
            {
                DeleteFolder(subFolder.Name, subFolder.ParentFolderId);
            }
        
            foreach (var file in folderToDelete.Files)
            {
                context.Files.Remove(file);
            }
        
            context.Folders.Remove(folderToDelete);
            context.SaveChanges();
        }
    }

    public static void DeleteFile(string fileName, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var fileToDelete = context.Files
                .FirstOrDefault(f => f.Name == fileName && f.FolderId == folderId);
            
            context.Files.Remove(fileToDelete);
            context.SaveChanges();
        }
    }

    public static void RenameFolder(string folderName, string newName, Guid? parentFolderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folderToRename = context.Folders
                .FirstOrDefault(f => f.Name == folderName && f.ParentFolderId == parentFolderId && f.UserId == userId);

            folderToRename.Name = newName;

            context.SaveChanges();
        }
    }

    public static void RenameFile(string fileName, string newName, Guid? folderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var fileToRename = context.Files
                .FirstOrDefault(f => f.Name == fileName && f.FolderId == folderId && f.UserId == userId);

            fileToRename.Name = newName;

            context.SaveChanges();
        }
    }

    public static void ShareFolder(Guid? parentFolderId, Guid usersId, Guid userId)
    {   
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.ParentFolderId == parentFolderId && f.UserId == userId);
            if (folder.UsersIds == null)
            {
                folder.UsersIds = new List<Guid>();
            }
            if (!folder.UsersIds.Contains(usersId) && folder.UserId != usersId)
            {
                folder.UsersIds.Add(usersId);
                context.SaveChanges();
            }
        }
    }

    public static void StopShare(string folderName, Guid? parentFolderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.ParentFolderId == parentFolderId && f.Name == folderName);
            folder.UsersIds.Remove(userId);
            context.SaveChanges();
        }
    }

    public static void ShareFile(string fileName, Guid? folderId, Guid usersId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var file = context.Files.FirstOrDefault(f => f.FolderId == folderId && f.Name == fileName);
            if (file.UsersIds == null)
            {
                file.UsersIds = new List<Guid>(); 
            }
            if (!file.UsersIds.Contains(usersId) && file.UserId != usersId)
            {
                file.UsersIds.Add(userId);
                context.SaveChanges();
            }
        }
    }

    public static void StopShareFile(string fileName, Guid? folderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var file = context.Files.FirstOrDefault(f => f.FolderId == folderId && f.Name == fileName);
            file.UsersIds.Remove(userId);
            context.SaveChanges();
        }
    }
    
}