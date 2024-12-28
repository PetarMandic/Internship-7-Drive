namespace Drive.Data.Entities.Models;

public class GroupFolderAndFile
{
    public Guid GroupId { get; init; }
    public Guid FolderId { get; init; }
    public Guid FileId { get; init; }
    
    public Folder Folder { get; init; }
    public File File { get; init; }
    
}