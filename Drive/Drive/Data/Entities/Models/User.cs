namespace Drive.Data.Entities.Models;

public class User
{
    public string Mail { get; set; }
    public string Password { get; set; }
    
    public User(string mail, string password)
    {
        Mail = mail;
        Password = password;
    }
}