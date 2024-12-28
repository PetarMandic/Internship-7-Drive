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
                new User(Guid.NewGuid(), "bartol.deak@example.com", "password123"),
                new User(Guid.NewGuid(), "ante.roca@example.com", "password456"),
                new User(Guid.NewGuid(), "matija.luketin@example.com", "password789"),
                new User(Guid.NewGuid(), "duje.saric@example.com", "password101"),
                new User(Guid.NewGuid(), "marija.sustic@example.com", "password102")
            });

        
        builder.Entity<Folder>()
            .HasData(new List<Folder>
            {
                new Folder(Guid.NewGuid(), "Documents"),
                new Folder(Guid.NewGuid(), "Images"),
                new Folder(Guid.NewGuid(), "Projects"),
            });
        
        builder.Entity<File>()
            .HasData(new List<File>
            {
                new File(Guid.NewGuid(),"Document1.txt", DateTime.UtcNow.AddDays(-10)),
                new File(Guid.NewGuid(),"Image1.jpg", DateTime.UtcNow.AddDays(-5)),
                new File(Guid.NewGuid(),"ProjectProposal.docx", DateTime.UtcNow.AddDays(-2)),
                new File(Guid.NewGuid(),"Document2.pdf", DateTime.UtcNow.AddDays(-8)),
                
            });
    }
}
