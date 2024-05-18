namespace ProjektZaliczeniowyPR3.Data;

using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowyPR3.Models;
using ProjektZaliczeniowyPR3.DatabaseConnection;


public class LibraryContext : DbContext
{
    public DbSet<Book> books { get; set; } = null!;
    public DbSet<User> users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConnectionInfo info = new();

        string connectionInfo = "Host=localhost:5432;Database=postgres;Username=postgres;Password=1234";
        //await info.RetrieveConnectionString();

        optionsBuilder.UseNpgsql(connectionInfo);
    }



    public LibraryContext(DbContextOptions options) : base(options) {}
    public LibraryContext() : base() {}


}