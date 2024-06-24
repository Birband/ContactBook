using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;

namespace ContactBook.Application.Services.Subcategories;

public interface ISubcategoryService
{
    Task<IEnumerable<Subcategory>> GetSubcategoriesAsync();
    Task AddSubcategoryAsync(Subcategory subcategory);
    Task DeleteSubcategoryAsync(Guid id);
    Task UpdateSubcategoryAsync(Subcategory subcategory);
}