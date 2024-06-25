using ContactBook.Domain.Entities;

namespace ContactBook.Application.Services.Contacts;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact?> GetContactByEmailAsync(string email);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(string email);
}