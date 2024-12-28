namespace Drive.Data.Entities.Models;

public class GroupUserAndFile
{
    public Guid GroupId { get; init; }
    public Guid UserId { get; init; }
    public Guid FileId { get; init; }
    
    public User User { get; set; }
    public File File { get; set; }
}