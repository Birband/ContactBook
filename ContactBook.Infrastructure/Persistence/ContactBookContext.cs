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
        
        List<Domain.Entities.Category> users = new()
        {
            new Domain.Entities.Category { Id = Guid.Parse("b6f0f09c-e503-4804-84cb-5bce1139e0d7"), Name = "Business" },
            new Domain.Entities.Category { Id = Guid.Parse("90ec091c-95fc-454d-9924-cf8e7fd2c217"), Name = "Private" },
            new Domain.Entities.Category { Id = Guid.Parse("9a9c5d7d-d124-45dc-9a59-ef6486cd42fc"), Name = "Other" }
        };
        // Add 3 categories to the database
        modelBuilder.Entity<Domain.Entities.Category>().HasData(
            users[0],
            users[1],
            users[2]
        );

        // Add 3 subcategories to the database
        modelBuilder.Entity<Domain.Entities.Subcategory>().HasData(
            new Domain.Entities.Subcategory { Id = Guid.Parse("9a71bcab-87cf-44b1-aa44-82f58ef0cb6f"), Name = "Business", CategoryId = Guid.Parse("b6f0f09c-e503-4804-84cb-5bce1139e0d7") },
            new Domain.Entities.Subcategory { Id = Guid.Parse("20f817b0-cfaa-43a0-b634-2242f82964f4"), Name = "Client", CategoryId = Guid.Parse("b6f0f09c-e503-4804-84cb-5bce1139e0d7") },
            new Domain.Entities.Subcategory { Id = Guid.Parse("5c861730-2b82-4012-ac6f-cfff4dcbaafc"), Name = "Home", CategoryId = Guid.Parse("9a9c5d7d-d124-45dc-9a59-ef6486cd42fc") },
            new Domain.Entities.Subcategory { Id = Guid.Parse("4bce6ac3-b6ed-488b-a759-64cafef966f7"), Name = "Mobile", CategoryId = Guid.Parse("9a9c5d7d-d124-45dc-9a59-ef6486cd42fc") }
        );
    }
}