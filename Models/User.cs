using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowyPR3.Data;
using ConnectionInfo = ProjektZaliczeniowyPR3.DatabaseConnection.ConnectionInfo;


namespace ProjektZaliczeniowyPR3.Models;
[Table("users")]
public class User 
{
    public int Id { get; set; }
    public string Username { get; set;} = null!;
    public string Password { get; set; } = null!;

    public byte[] salt { get; set; } = null!;

    public bool isAdmin { get; set;} = false;
 
    public User()
    {}
    public User(string username, string password, bool isadmin) {
        Username = username;
        Password = password;
        isAdmin = isadmin;
    }

    public async void RemoveUser()
    {
        ConnectionInfo info = new();
        if (!info.TryConnection())
            return;
        
        using (var db = new LibraryContext())
        {
            List<Book> books = db.books.ToList();
            
            foreach (var book in books)
            {
                if (book.UserId == this.Id)
                    book.UserId = -1;
            }

            db.Entry(this).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }
    }

    public async void AddUser()
    {
        ConnectionInfo info = new();
        if (!info.TryConnection())
            return;
        using (var db = new LibraryContext())
        {
            db.users.Add(this);
            await db.SaveChangesAsync();
        }
        
    }
    
}