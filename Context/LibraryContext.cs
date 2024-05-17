using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowyPR3.Models;

namespace ProjektZaliczeniowyPR3.Data;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionInfo = "Host=localhost:5432;Database=postgres;Username=postgres;Password=1234";
        optionsBuilder.UseNpgsql(connectionInfo);
    }

    public LibraryContext(DbContextOptions options) : base(options) {}
    public LibraryContext() : base() {}


}