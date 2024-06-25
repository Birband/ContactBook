using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid id);
    Task UpdateCategoryAsync(Category category);
}
