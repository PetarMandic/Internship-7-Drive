using Drive.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;

namespace Drive.Domain.Repositories;

public class UserRepository 
{
    public static void AddUser(string mail, string password)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var newUser = new User(Guid.NewGuid(), mail, password);
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}