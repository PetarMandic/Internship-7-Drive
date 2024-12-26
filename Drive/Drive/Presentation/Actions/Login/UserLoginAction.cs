using Drive.Presenation.Helpers;

namespace Drive.Presenation.Actions.Login;

public class UserLoginAction
{
    public static void UserLogin()
    {
        Writer.WriteMail();
        Reader.TryReadMail();
        Writer.WritePassword();
        Reader.TryReadPassword();
    }
}