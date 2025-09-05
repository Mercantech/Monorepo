using Microsoft.EntityFrameworkCore;

namespace Blazor.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Her kan du tilføje dine DbSet properties senere når du opretter modeller
    // Eksempel:
    // public DbSet<User> Users { get; set; }
    // public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Her kan du konfigurere dine modeller senere
        // Eksempel:
        // modelBuilder.Entity<User>().HasKey(u => u.Id);
        // modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
    }
}
