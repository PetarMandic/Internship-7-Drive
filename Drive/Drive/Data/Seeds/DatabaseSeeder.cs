using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Data.Seeds;

public static class DatabaseSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasData(new List<User>
            {
                new User(new Guid(), "bartol.deak@example.com", "password123"),
                new User(new Guid(), "ante.roca@example.com", "password456"),
                new User(new Guid(), "matija.luketin@example.com", "password789"),
                new User(new Guid(), "duje.saric@example.com", "password101"),
                new User(new Guid(), "marija.sustic@example.com", "password102")
            });

        
        builder.Entity<Folder>()
            .HasData(new List<Folder>
            {
                new Folder(new Guid(), "Documents"),
                new Folder(new Guid(), "Images"),
                new Folder(new Guid(), "Projects"),
            });
        
        builder.Entity<File>()
            .HasData(new List<File>
            {
                new File(new Guid(),"Document1.txt", DateTime.UtcNow.AddDays(-10)),
                new File(new Guid(),"Image1.jpg", DateTime.UtcNow.AddDays(-5)),
                new File(new Guid(),"ProjectProposal.docx", DateTime.UtcNow.AddDays(-2)),
                new File(new Guid(),"Document2.pdf", DateTime.UtcNow.AddDays(-8)),
                
            });
    }
}
