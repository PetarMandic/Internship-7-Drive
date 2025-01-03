using Drive.Domain.Repositories;
using Drive.Presentation.Helpers;

namespace Drive.Presentation.Actions.DriveMenu.MyDisk;

public class CommentAction
{
    public static void Comment(string fileName, Guid? folderId, string email)
    {
        var userId = CommentRepository.ReturnId(email);
        var documentId = CommentRepository.Return(fileName, userId, folderId);
        Writer.PrintComments(documentId);
        
        Console.WriteLine("Unesite komandu: ");
        var command = Reader.TryReadCommand();

        var commandValid = Command(command, folderId, email, userId, documentId, fileName);

        if (!commandValid)
        {
            Console.Clear();
            Console.WriteLine("Niste unijeli ispravno komandu");
            Comment(fileName, folderId, email);
        }
        
    }

    public static bool Command(string command, Guid? parentFolderId,string mail, Guid userId, Guid documentId, string fileName)
    {
        var commandLine = command;
        command = CommentRepository.ReturnCommand(command);
        switch (command)
        {
            case "help":
                Console.Clear();
                break;
            case "dodaj komentar":
                Console.Clear();
                AddComment(userId, documentId, parentFolderId, mail, fileName);
                break;
            case "uredi komentar":
                Console.Clear();
                EditComment(userId, documentId, parentFolderId, mail, fileName);
                break;
            case "izbriši komentar":
                Console.Clear();
                RemoveComment(userId, documentId, parentFolderId, mail, fileName);
                break;
            case "povratak":
                Console.Clear();
                MyDiskAction.MyDisk(mail, parentFolderId);
                break;
        }

        return false;
    }

    public static void AddComment(Guid userId, Guid documentId, Guid? parentFolderId, string email, string fileName)
    {
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
                var isValid2 = CommentRepository.Finished(line);
                if (isValid2)
                {
                    line = line.Replace("(:)gotovo", "");
                    CommentRepository.CreateComment(userId, documentId);
                    Console.WriteLine("Komentar uspiješno dodan");
                    Comment(fileName, parentFolderId, email);
                }
                Console.Clear();
                Console.WriteLine(line);
            }
        }
    }

    public static void EditComment(Guid userId, Guid documentId, Guid? parentFolderId, string email, string fileName)
    {
        var text = new List<string>();
        var line = "";
        Console.WriteLine("Unesite id komentara: ");
        var commentsId = Console.ReadLine();
        Guid commentId;
        if (Guid.TryParse(commentsId, out commentId))
        {
            text = CommentRepository.ReturnComment(commentId);
        }
        else
        {
            Console.WriteLine("Neispravan id komentara");
            Thread.Sleep(1500);
            Comment(fileName, parentFolderId, email);
        }

        for (int i = 0; i <= text.Count; i++)
        {
            Console.WriteLine(text[i]);
            line = text[i];
        }
        
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
                var isValid2 = CommentRepository.Finished(line);
                if (isValid2)
                {
                    line = line.Remove(line.Length - 8, line.Length);
                    CommentRepository.SaveComment(commentId, text);
                    Console.WriteLine("Komentar je uređen");
                    Comment(fileName, parentFolderId, email);
                }
                Console.Clear();
                Console.WriteLine(line);
            }
        }
    }

    public static void RemoveComment(Guid userId, Guid documentId, Guid? parentFolderId, string email, string fileName)
    {
        Console.WriteLine("Unesite id komentara: ");
        var commentsId = Console.ReadLine();
        Guid commentId;
        if (Guid.TryParse(commentsId, out commentId))
        {
            Console.WriteLine("Komentar je uspiješno izbrisan");
            CommentRepository.DeleteComment(commentId);
            Comment(fileName, parentFolderId, email);
        }
        else
        {
            Console.WriteLine("Neispravan id komentara");
            Thread.Sleep(1500);
            Comment(commentsId, parentFolderId, commentsId);
        }
    }
}