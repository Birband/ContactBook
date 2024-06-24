using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;

namespace ContactBook.Application.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid id);
    Task UpdateCategoryAsync(Category category);
}