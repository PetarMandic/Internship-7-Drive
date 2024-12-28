using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;

namespace Drive.Domain.Repositories;

public class LoginRepository
{
    private static DateTime? lastLoginAttempt = null;
    private static readonly TimeSpan cooldownPeriod = TimeSpan.FromSeconds(30); 
    
    public static bool MailExist(string email)
    {
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            return context.Users.Any(u => u.Mail == email);
        }
    }

    public static bool PasswordMatch(string password)
    {
        lastLoginAttempt = DateTime.Now;
        using (var context = new DriveDbContext(new DbContextOptionsBuilder<DriveDbContext>().Options))
        {
            return context.Users.Any(u => u.Password == password);
        }
        
    }
    
    public static TimeSpan TimePassed()
    {
        if (lastLoginAttempt.HasValue && DateTime.Now - lastLoginAttempt.Value < cooldownPeriod)
        {
            TimeSpan remainingTime = cooldownPeriod - (DateTime.Now - lastLoginAttempt.Value);
            return remainingTime;
        }
        
        return TimeSpan.Zero;
    }
}