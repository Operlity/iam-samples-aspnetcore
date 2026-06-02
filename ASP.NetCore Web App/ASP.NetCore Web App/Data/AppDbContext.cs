using Microsoft.EntityFrameworkCore;
using ASP.NetCore_Web_App.Models;

namespace ASP.NetCore_Web_App.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure indexes or custom settings if needed
        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.CreatedBy); // Index for performance on queries filtering by user
    }
}
