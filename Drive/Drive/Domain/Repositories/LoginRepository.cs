using Drive.Data;

namespace Drive.Domain.Repositories;

public class LoginRepository
{
    private static DateTime? lastLoginAttempt = null;
    private static readonly TimeSpan cooldownPeriod = TimeSpan.FromSeconds(30); 
    
    public static bool MailExist(string email)
    {
        return false;
    }

    public static bool PasswordMatch(string password)
    {
        lastLoginAttempt = DateTime.Now;
        return false;
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