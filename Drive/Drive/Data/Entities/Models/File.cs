using System.Net.Mime;

namespace Drive.Data.Entities.Models;

public class File
{
    public Guid Id { get; init; }
    public string FileName { get; set; }
    public DateTime EditingTime { get; set; }

    public File(Guid id, string fileName, DateTime editingTime)
    {
        Id = id;
        FileName = fileName;
        EditingTime = editingTime;
    }
}