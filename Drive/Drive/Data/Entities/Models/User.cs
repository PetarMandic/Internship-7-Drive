namespace Drive.Data.Entities.Models;

public class User
{
    public Guid Id { get; init; }
    public string Mail { get; set; }
    public string Password { get; set; }
    
    public User(Guid id, string mail, string password)
    {
        Id = id;
        Mail = mail;
        Password = password;
    }
    public ICollection<File> Files { get; set; }
    public ICollection<Folder> Folders { get; set; } = new List<Folder>();
    public ICollection<Comment> Comments { get; set; }  
}