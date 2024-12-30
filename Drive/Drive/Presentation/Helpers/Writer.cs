using Drive.Data.Entities.Models;
using Drive.Presentation.Actions.DriveMenu.MyDisk;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Presentation.Helpers;

public class Writer
{
    public static void WriteMail()
        => Console.WriteLine("Unesite mail: ");

    public static void WritePassword()
        => Console.WriteLine("Unesite lozinku: ");

    public static void PrintFoldersAndFiles(List<Folder> folders, List<File> files)
    {
        foreach (var folder in folders)
        {
            Console.WriteLine($"Mapa: {folder.Name}");
        }

        foreach (var file in files)
        {
            Console.WriteLine($"Datoteka: {file.Name}, Zadnje vrijeme uređivanja: {file.EditingTime}");
        }
    }

    public static void HelpCommand()
    {
        Console.WriteLine("help - za ispis svih komandi\n stvori mapu ‘ime mape’ – za stvaranje mape na trenutnoj lokaciji\n stvori datoteku ‘ime datoteke’ – za stvaranje datoteke na trenutnojlokaciji\n uđi u mapu ‘ime mape’ – za ulazak u mapu\n uredi datoteku ‘ime datoteke’\n izbriši mapu/datoteku ‘ime mape/datoteke’\n promjeni naziv mape/datoteke ‘ime mape/datoteke’ u ‘novo ime\nmape/datoteke’\n povratak");
    }

    public static void EditingHelpCommand()
    {
        Console.WriteLine("help – ispisuju se sve komande za uređivanje datoteke\nspremanje i izlaz\nizlaz bez spremanja");
    }

    public static void DoesntExist(bool nameExist, string type, string mail)
    {
        if (nameExist == false)
        {
            Console.WriteLine($"Ime {type} nije ispravao");
            MyDiskAction.MyDisk(mail);
        }
    }
}