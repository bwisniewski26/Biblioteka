using System.ComponentModel.DataAnnotations.Schema;

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
}