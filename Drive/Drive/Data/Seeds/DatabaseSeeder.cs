using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Data.Seeds
{
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
            
            var bartol = users[0];
            var ante = users[1];
            var matija = users[2];
            var duje = users[3];
            var marija = users[4];
            
            builder.Entity<User>().HasData(users);
            
            builder.Entity<Folder>()
                .HasData(new List<Folder>
                {
                    new Folder(Guid.NewGuid(), "Documents", null, bartol.Id, new List<Guid> { ante.Id}),  
                    new Folder(Guid.NewGuid(), "Images", null, ante.Id, new List<Guid> { bartol.Id}),       
                    new Folder(Guid.NewGuid(), "Projects", null, matija.Id, new List<Guid> { bartol.Id}), 
                });
            
            builder.Entity<File>()
                .HasData(new List<File>
                {
                    new File(Guid.NewGuid(), "Document1.txt", DateTime.UtcNow.AddDays(-10), new List<string>{"dokument1"}, null, bartol.Id, new List<Guid> { ante.Id}),  
                    new File(Guid.NewGuid(), "Image1.jpg", DateTime.UtcNow.AddDays(-5), new List<string>{"najbolja slika"}, null, ante.Id, new List<Guid> { bartol.Id}),  
                    new File(Guid.NewGuid(), "ProjectProposal.docx", DateTime.UtcNow.AddDays(-2), new List<string>{"prvi projekt"}, null, matija.Id, new List<Guid> { bartol.Id}), 
                    new File(Guid.NewGuid(), "Document2.pdf", DateTime.UtcNow.AddDays(-8), new List<string>{"dokument2"}, null, duje.Id, new List<Guid> { matija.Id }), 
                });
        }
    }
}
