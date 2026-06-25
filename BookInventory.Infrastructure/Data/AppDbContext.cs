using BookInventory.Application.Interfaces;
using BookInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // map DateOnly to PostgreSQL 'date' type
        modelBuilder.Entity<Book>(entity => {
            entity.Property(b => b.CreatedAt).HasColumnType("date");
            entity.Property(b => b.ReadingStartedDate).HasColumnType("date");
            entity.Property(b => b.DateFinished).HasColumnType("date");
        });

        modelBuilder.Entity<User>(entity => {
            entity.HasIndex(u => u.Email).IsUnique();
        });
    }
}