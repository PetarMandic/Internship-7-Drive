namespace Drive.Data.Entities.Models;

public class GroupUserAndFolder
{
    
    public Guid GroupId { get; init; }
    public Guid UserId { get; init; }
    public Guid FolderId { get; init; }
    
    public User User { get; set; }
    public Folder Folder { get; set; }
    
    
}