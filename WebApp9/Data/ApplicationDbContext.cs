using Microsoft.EntityFrameworkCore;
using WebApp9.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка индексов и ограничений
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Author);
    }
}