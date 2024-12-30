using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Data.Seeds;

public static class DatabaseSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        var users = new List<User>
        {
            new User(Guid.NewGuid(), "bartol.deak@example.com", "password123"),
            new User(Guid.NewGuid(), "ante.roca@example.com", "password456"),
            new User(Guid.NewGuid(), "matija.luketin@example.com", "password789"),
            new User(Guid.NewGuid(), "duje.saric@example.com", "password101"),
            new User(Guid.NewGuid(), "marija.sustic@example.com", "password102")
        };
    
        builder.Entity<User>().HasData(users);
        
        builder.Entity<Folder>()
            .HasData(new List<Folder>
            {
                new Folder(Guid.NewGuid(), "Documents", null) { UserId = users[0].Id }, 
                new Folder(Guid.NewGuid(), "Images", null) { UserId = users[1].Id },    
                new Folder(Guid.NewGuid(), "Projects", null) { UserId = users[2].Id }   
            });
        
        builder.Entity<File>()
            .HasData(new List<File>
            {
                new File(Guid.NewGuid(),"Document1.txt", DateTime.UtcNow.AddDays(-10), "Doument", null),
                new File(Guid.NewGuid(),"Image1.jpg", DateTime.UtcNow.AddDays(-5), "Slika", null),
                new File(Guid.NewGuid(),"ProjectProposal.docx", DateTime.UtcNow.AddDays(-2), "Projekt", null),
                new File(Guid.NewGuid(),"Document2.pdf", DateTime.UtcNow.AddDays(-8), "Dokument", null),
            });
    }
}
