using ContactBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence.Repositories;

/// <summary>
/// Category repository
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly ContactBookDbContext _context;

    public CategoryRepository(ContactBookDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Add a new category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        // Get also their subcategories
        return await _context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
}