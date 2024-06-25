using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;

namespace ContactBook.Application.Services.Categories;

/// <summary>
/// Category service
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetCategoriesAsync();
    }

    /// <summary>
    /// Add a new category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task AddCategoryAsync(Category category)
    {
        await _categoryRepository.AddCategoryAsync(category);
    }

    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteCategoryAsync(Guid id)
    {
        await _categoryRepository.DeleteCategoryAsync(id);
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task UpdateCategoryAsync(Category category)
    {
        await _categoryRepository.UpdateCategoryAsync(category);
    }
}