using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence;

public class ContactBookDbContext : DbContext
{
    public ContactBookDbContext(DbContextOptions<ContactBookDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.User> Users { get; set; }
}