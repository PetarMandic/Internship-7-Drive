using Drive.Data.Entities;
using Drive.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Drive.Domain.Repositories;

public class CommentRepository
{

    public static Guid ReturnId(string mail)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == mail);
            return user.Id;
        }
    }
    
    public static Guid Return(string fileName, Guid userId, Guid? folderId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var file = context.Files.FirstOrDefault(f => f.Name == fileName && (f.UsersIds.Contains(userId) || f.UserId == userId) && f.FolderId == folderId);
            return file.Id;
        }
    }
    
    public static Dictionary<int, (Guid, string, DateTime, List<string>)> GetComments(Guid DocumentId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var comments = context.Comments
                .Where(c => c.DocumentId == DocumentId)
                .ToList();
            
            Dictionary<int, (Guid id, string mail, DateTime time, List<string> text)> commentsToReturn = new Dictionary<int, (Guid, string, DateTime, List<string>)>();

            int i = 0;
            foreach (var comment in comments)
            {
                var user = context.Users.FirstOrDefault(c => c.Id == comment.UserId);

                if (user != null)
                {
                    var mail = user.Mail;
                    
                    commentsToReturn.Add(i, (comment.DocumentId, mail, comment.Time, comment.Comments));
                    i++;
                }
            }

            return commentsToReturn;
        }
    }
    
    public static string ReturnCommand(string currentLine)
    {
        if (currentLine.Contains("help") || currentLine.Contains("dodaj komentar") || currentLine.Contains("uredi komentar") || 
            currentLine.Contains("izbriši komentar") || currentLine.Contains("povratak"))
        {
            var line = currentLine;  

            var command = line switch
            {
                var l when l.Contains("help") => "help",
                var l when l.Contains("dodaj komentar") => "dodaj komentar",
                var l when l.Contains("uredi komentar") => "uredi komentar",
                var l when l.Contains("izbriši komentar") => "izbriši komentar",
                var l when l.Contains("povratak") => "povratak",
                _ => null
            };

            return command;
        }

        return null;
    }

    public static bool Finished(string currentLine)
    {
        if (currentLine.Contains("(:)gotovo"))
        {
            return true;
        }
        return false;
    }

    public static void CreateComment(Guid userId, Guid documentId)
    {
        
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var comment = new Comment(Guid.NewGuid(), userId, documentId, DateTime.UtcNow,Lists.newText);
            context.Comments.Add(comment);
            context.SaveChanges();
        }
        Lists.newText.Clear();
    }

    public static List<string> ReturnComment(Guid commentId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var comment = context.Comments.FirstOrDefault(c => c.Id == commentId);
            return comment.Comments;
        }
    }

    public static void SaveComment(Guid commentId, List<string> comments)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var comment = context.Comments.FirstOrDefault(c => c.Id == commentId);
            comment.Comments = comments;
            context.SaveChanges();
        }
    }

    public static void DeleteComment(Guid commentId)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var comment = context.Comments.FirstOrDefault(c => c.Id == commentId);
            context.Comments.Remove(comment);
            context.SaveChanges();
        }
    }
}