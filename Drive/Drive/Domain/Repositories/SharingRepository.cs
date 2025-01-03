using Drive.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Domain.Repositories;

public class SharingRepository
{
    public static void DeleteSharedFolder(string folderName, Guid? parentFolderId, Guid userId)
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
                DeleteSharedFolder(subFolder.Name, subFolder.ParentFolderId, userId);
            }

            if (folderToDelete.Files.ToList().Count > 0)
            {
                foreach (var file in folderToDelete.Files)
                {
                    file.UsersIds.Remove(userId);   
                }
            }
            
            foreach (var folders in folderToDelete.SubFolders)
            {
                folders.UsersIds.Remove(userId);
            }
            
            context.SaveChanges();
        }        
    }
    
    public static void DeleteSharedFile(string fileName, Guid? folderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var fileToDelete = context.Files
                .FirstOrDefault(f => f.Name == fileName && f.FolderId == folderId);

            fileToDelete.UsersIds.Remove(userId);
            context.SaveChanges();
        }
    }
    
    public static Guid? OpenFolder(string folderName, Guid? parentFolderId, Guid userId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.Name == folderName && f.ParentFolderId == parentFolderId && f.UsersIds.Contains(userId));
            return folder.Id;
        }
    }
    
    public static List<Folder> FindFolders(Guid userId, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folders = context.Folders
                .Where(f => f.ParentFolderId == folderId && f.UsersIds.Contains(userId)) 
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
                .Where(f => f.FolderId == folderId && f.UsersIds.Contains(userId)) 
                .OrderBy(f => f.EditingTime) 
                .ToList();
            return files;
        }
    }
    
    public static void CreateFolder(string nameOfFolder, Guid? parentFolderId, Guid userId, Guid usersId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var newFolder = new Folder(Guid.NewGuid(), nameOfFolder, parentFolderId, usersId, new List<Guid>{userId});
            context.Folders.Add(newFolder);
            context.SaveChanges();
        }
    }
    
    public static void CreateFile(string nameOfFile, Guid? parentFolderId, Guid userId, Guid usersId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var newFile = new File(Guid.NewGuid(), nameOfFile,DateTime.UtcNow , new List<string>(){""},parentFolderId, usersId,new List<Guid>{userId});
            context.Files.Add(newFile);
            context.SaveChanges();
        }
    }

    public static Guid FindOwner(Guid? parentFolderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var folder = context.Folders.FirstOrDefault(f => f.Id == parentFolderId);
            return folder.UserId;
        }
    }
}