using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;

namespace Drive.Domain.Repositories;

public class ProfileSettingsRepository
{
    public static void ChangeMail(string mail, string newMail)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == mail);
            user.Mail = newMail;
            context.Users.Update(user);
            context.SaveChanges();
        }
    }

    public static void ChangePassword(string password, string mail, string newPassword)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == mail && u.Password == password);
            user.Password = newPassword;
            context.Users.Update(user);
            context.SaveChanges();
        }
    }

    public static string FindUserPassword(string mail)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == mail);
            return user.Password;
        }
    }
}