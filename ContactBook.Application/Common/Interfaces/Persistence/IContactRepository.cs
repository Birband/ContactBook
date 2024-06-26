using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact?> GetContactByEmailAsync(string id);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(string email);
}
