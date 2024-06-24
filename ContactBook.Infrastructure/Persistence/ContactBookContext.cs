using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence;

public class ContactBookDbContext : DbContext
{
    public ContactBookDbContext(DbContextOptions<ContactBookDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Domain.Entities.Contact> Contacts { get; set; }
    public DbSet<Domain.Entities.Subcategory> Subcategories { get; set; }
    public DbSet<Domain.Entities.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Category>()
            .HasMany(c => c.Subcategories)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}