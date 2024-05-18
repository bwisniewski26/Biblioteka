namespace ProjektZaliczeniowyPR3.Models;

public class User 
{
    public int Id { get; set; }
    public string Username { get; set;} = null!;
    public string Password { get; set; } = null!;

    public bool isAdmin { get; set;} = false;
 
    public ICollection<Book> Books { get; set;} = null!;
 
    public User()
    {}
    public User(string username, string password, bool isadmin) {
        Username = username;
        Password = password;
        isAdmin = isadmin;
    }

    public void SetPassword(string newPassword)
    {
        Password = newPassword;
    }

}