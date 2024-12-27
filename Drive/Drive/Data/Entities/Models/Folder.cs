namespace Drive.Data.Entities.Models;

public class Folder
{
    public Guid Id { get; init; }
    public string FolderName { get; set; }

    public Folder(Guid id, string folderName)
    {
        Id = id;
        FolderName = folderName;
    }
    
}