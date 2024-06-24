using ContactBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly ContactBookDbContext _context;

    public SubcategoryRepository(ContactBookDbContext context)
    {
        _context = context;
    }

    public async Task AddSubcategoryAsync(Subcategory subcategory)
    {
        await _context.Subcategories.AddAsync(subcategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubcategoryAsync(Guid id)
    {
        var subcategory = await _context.Subcategories.FindAsync(id);
        if (subcategory != null)
        {
            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync()
    {
        return await _context.Subcategories.ToListAsync();
    }

    public async Task UpdateSubcategoryAsync(Subcategory subcategory)
    {
        _context.Subcategories.Update(subcategory);
        await _context.SaveChangesAsync();
    }
}