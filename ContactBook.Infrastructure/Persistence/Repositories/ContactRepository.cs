using ContactBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ContactBookDbContext _context;

    public ContactRepository(ContactBookDbContext context)
    {
        _context = context;
    }   

    public async Task AddContactAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContactAsync(string email)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Contact?> GetContactByEmailAsync(string email)
    {
        return await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }
}
