using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;

namespace ContactBook.Application.Services.Contacts;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return await _contactRepository.GetContactsAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(Guid id)
    {
        return await _contactRepository.GetContactByIdAsync(id);
    }

    public async Task AddContactAsync(Contact contact)
    {
        await _contactRepository.AddContactAsync(contact);
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        await _contactRepository.UpdateContactAsync(contact);
    }

    public async Task DeleteContactAsync(Guid id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }
}