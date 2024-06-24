using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public interface IContactRepository
{
    // add, delete, update, get contacts, get contact by id
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact?> GetContactByIdAsync(Guid id);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(Guid id);
}
