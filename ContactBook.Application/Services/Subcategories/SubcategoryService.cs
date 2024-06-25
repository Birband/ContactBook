using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;

namespace ContactBook.Application.Services.Subcategories;

/// <summary>
/// Subcategory service
/// </summary>
public class SubcategoryService : ISubcategoryService
{
    private readonly ISubcategoryRepository _subcategoryRepository;

    public SubcategoryService(ISubcategoryRepository subcategoryRepository)
    {
        _subcategoryRepository = subcategoryRepository;
    }

    /// <summary>
    /// Get all subcategories
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync()
    {
        return await _subcategoryRepository.GetSubcategoriesAsync();
    }
    
    /// <summary>
    /// Add a new subcategory
    /// </summary>
    /// <param name="subcategory"></param>
    /// <returns></returns>
    public async Task AddSubcategoryAsync(Subcategory subcategory)
    {
        await _subcategoryRepository.AddSubcategoryAsync(subcategory);
    }

    /// <summary>
    /// Delete a subcategory
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteSubcategoryAsync(Guid id)
    {
        await _subcategoryRepository.DeleteSubcategoryAsync(id);
    }

    /// <summary>
    /// Update a subcategory
    /// </summary>
    /// <param name="subcategory"></param>
    /// <returns></returns>
    public async Task UpdateSubcategoryAsync(Subcategory subcategory)
    {
        await _subcategoryRepository.UpdateSubcategoryAsync(subcategory);
    }
}