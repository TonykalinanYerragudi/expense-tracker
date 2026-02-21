using ExpenseTracker.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}