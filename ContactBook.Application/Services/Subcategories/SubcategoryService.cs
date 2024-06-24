using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;

namespace ContactBook.Application.Services.Subcategories;

public class SubcategoryService : ISubcategoryService
{
    private readonly ISubcategoryRepository _subcategoryRepository;

    public SubcategoryService(ISubcategoryRepository subcategoryRepository)
    {
        _subcategoryRepository = subcategoryRepository;
    }

    public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync()
    {
        return await _subcategoryRepository.GetSubcategoriesAsync();
    }

    public async Task AddSubcategoryAsync(Subcategory subcategory)
    {
        await _subcategoryRepository.AddSubcategoryAsync(subcategory);
    }

    public async Task DeleteSubcategoryAsync(Guid id)
    {
        await _subcategoryRepository.DeleteSubcategoryAsync(id);
    }

    public async Task UpdateSubcategoryAsync(Subcategory subcategory)
    {
        await _subcategoryRepository.UpdateSubcategoryAsync(subcategory);
    }
}