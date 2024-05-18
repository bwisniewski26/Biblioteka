namespace ProjektZaliczeniowyPR3.Models;
public class Book
{
    
    public int Id { get; set; }
    public string Title { get; set;} = null!;
    public string Author { get; set; } = null!;
    public string? Description { get; set; }

    public bool isAvailable { get; set; } = false;


    public Book()
    {

    }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}