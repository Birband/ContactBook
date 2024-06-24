using ContactBook.Domain.Entities;

namespace ContactBook.Application.Services.Contacts;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact?> GetContactByIdAsync(Guid id);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(Guid id);
}