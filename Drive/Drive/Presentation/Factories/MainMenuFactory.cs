namespace Drive.Presenation.Factories;

public class MainMenuFactory
{
    public static List<string> CreateActions()
    {
        return new List<string>() { "1 -> Login korisnika", "2 -> Registracija korisnika", "3 -> Izlaz" };
    }
}