using System.Net.Mime;

namespace Drive.Data.Entities.Models;

public class File
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public DateTime EditingTime { get; set; }
    public List<string> Text { get; set; }

    public File(Guid id, string name, DateTime editingTime, List<string> text, Guid? folderId)
    {
        Id = id;
        Name = name;
        EditingTime = editingTime;
        Text = text;
        FolderId = folderId;
    }
    
    public Guid? FolderId { get; set; }
    public Folder Folder { get; set; }
}