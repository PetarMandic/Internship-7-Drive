namespace Drive.Presenation.Factories;

public class DriveMenuFactory
{
    public static List<string> CreateAction()
    {
        return new List<string>() { "Moj disk", "Dijeljeno samnom", "Postavke profila", "Odjava iz profila" };
    }
}