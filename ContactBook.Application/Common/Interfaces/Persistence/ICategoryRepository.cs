using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public interface ICategoryRepository
{
    // add, delete, update, get contacts, get contact by id
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid id);
    Task UpdateCategoryAsync(Category category);
}
