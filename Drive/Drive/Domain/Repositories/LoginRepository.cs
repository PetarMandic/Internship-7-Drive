using Drive.Data;

namespace Drive.Domain.Repositories;

public class LoginRepository
{
    public static bool MailFormatValid(string email)
    {
        var condition = false;

        BasicData.stringSymbols = new List<char>(email.ToCharArray());

        static void FirstCheck()
        {
            
        }

        static void SecondCheck()
        {
            
        }

        static void ThirdCheck()
        {
            
        }
        
        return condition;
    }
}