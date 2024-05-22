using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowyPR3.Data;
using ProjektZaliczeniowyPR3.Models;

namespace ProjektZaliczeniowyPR3.DatabaseConnection;

public class Tables
{
    private ConnectionInfo info = new();

    public void RemoveUsers()
    {
        if (!info.TryConnection())
            return;
        using (var db = new LibraryContext())
        {
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE users");
        }
        
    }

    public void RemoveBooks()
    {
        if (!info.TryConnection())
            return;
        using (var db = new LibraryContext()) 
        { 
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE books");
        }
    }

    public List<Book> ListBooks()
    {
        List<Book> books = new();
        using (var db = new LibraryContext())
        {
            books = db.books
                .ToList();
        }
        return books;
    }

    public List<User> ListUsers()
    {
        List<User> users = new();
        using (var db = new LibraryContext())
        {
            users = db.users
                .ToList();
        }
        return users;
    }

    public User RetrieveUserById(int Id)
    {
        User notLogged = new();
        notLogged.Id = -1;
        using (var db = new LibraryContext())
        {
            try
            {
                User LoggedUser = db.users
                    .Single(loggedUser => loggedUser.Id == Id);
                return LoggedUser;
            }
            catch
            {
                return notLogged;
            }
        }
    }
}