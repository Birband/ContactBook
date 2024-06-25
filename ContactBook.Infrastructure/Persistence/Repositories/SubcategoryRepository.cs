using ContactBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence.Repositories;

/// <summary>
/// Subcategory repository
/// </summary>
public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly ContactBookDbContext _context;

    public SubcategoryRepository(ContactBookDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Add a new subcategory
    /// </summary>
    /// <param name="subcategory"></param>
    /// <returns></returns>
    public async Task AddSubcategoryAsync(Subcategory subcategory)
    {
        await _context.Subcategories.AddAsync(subcategory);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a subcategory
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteSubcategoryAsync(Guid id)
    {
        var subcategory = await _context.Subcategories.FindAsync(id);
        if (subcategory != null)
        {
            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Get all subcategories
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync()
    {
        return await _context.Subcategories.ToListAsync();
    }

    /// <summary>
    /// Update a subcategory
    /// </summary>
    /// <param name="subcategory"></param>
    /// <returns></returns>
    public async Task UpdateSubcategoryAsync(Subcategory subcategory)
    {
        _context.Subcategories.Update(subcategory);
        await _context.SaveChangesAsync();
    }
}