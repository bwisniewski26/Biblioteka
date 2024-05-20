using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektZaliczeniowyPR3.Models;
[Table("users")]
public class User 
{
    public int Id { get; set; }
    public string Username { get; set;} = null!;
    public string Password { get; set; } = null!;

    public byte[] salt { get; set; } = null!;

    public bool isAdmin { get; set;} = false;
 
    public List<Book> Books { get; set;} = null!;
 
    public User()
    {}
    public User(string username, string password, bool isadmin) {
        Username = username;
        Password = password;
        isAdmin = isadmin;
    }

}