namespace Drive.Data.Entities.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DocumentId { get; set; }
        public DateTime Time { get; set; }
        public List<string> Comments { get; set; }
        
        public Comment(Guid id, Guid userId, Guid documentId, DateTime time, List<string> comments)
        {
            Id = id;
            UserId = userId;
            DocumentId = documentId;
            Time = time;
            Comments = comments;
        }
        
        public User User { get; set; }
        public File File { get; set; } 
    }
}