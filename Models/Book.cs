using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowyPR3.Data;
using ConnectionInfo = ProjektZaliczeniowyPR3.DatabaseConnection.ConnectionInfo;

namespace ProjektZaliczeniowyPR3.Models;
[Table("books")]
public class Book
{
    
    public int Id { get; set; }
    public string Title { get; set;} = null!;
    public string Author { get; set; } = null!;
    public string? Description { get; set; }

    public bool isAvailable { get; set; } = true;

    public int? UserId { get; set; } = null;


    public Book()
    {

    }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        string Availability = isAvailable ? "Dostepna" : "Niedostepna";
        return Id.ToString() + " " + Title + " " + Author + " " + Description + " " + Availability;
    }

    public bool InsertBook()
    {
        ConnectionInfo info = new();
        if (!info.TryConnection())
        {
            return false;
        }
        using (var db = new LibraryContext())
        {
            db.books.Add(this);
            db.SaveChanges();
        }

        return true;
    }

    public bool BorrowBook(int userId)
    {
        ConnectionInfo info = new();
        if (!info.TryConnection())
        {
            return false;
        }

        this.UserId = userId;
        this.isAvailable = false;
        using (var db = new LibraryContext())
        {
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();
        }

        return true;
    }

    public bool ReturnBook()
    {
        ConnectionInfo info = new();
        if (!info.TryConnection())
        {
            return false;
        }

        this.UserId = -1;
        this.isAvailable = true;
        using (var db = new LibraryContext())
        {
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();
        }

        return true;
    }

    public async void RemoveBook()
    {
        ConnectionInfo info = new();
        if (!info.TryConnection())
        {
            return;
        }

        using (var db = new LibraryContext())
        {
            db.Entry(this).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }
    }
}