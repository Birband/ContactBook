using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public interface ISubcategoryRepository
{
    Task<IEnumerable<Subcategory>> GetSubcategoriesAsync();
    Task AddSubcategoryAsync(Subcategory subcategory);
    Task DeleteSubcategoryAsync(Guid id);
    Task UpdateSubcategoryAsync(Subcategory subcategory);

}
