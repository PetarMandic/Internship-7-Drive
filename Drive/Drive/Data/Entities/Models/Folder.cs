namespace Drive.Data.Entities.Models;

public class Folder
{
    public Guid Id { get; init; }
    public string Name { get; set; }

    public Folder(Guid id, string name, Guid? parentFolderId)
    {
        Id = id;
        Name = name;
        ParentFolderId = parentFolderId;
    }
    
    public Guid? ParentFolderId { get; set; } 
    public Folder ParentFolder { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Folder> SubFolders { get; set; } = new List<Folder>();
    public ICollection<File> Files { get; set; }
    
}