using ContactBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence.Repositories;

/// <summary>
/// Contact repository
/// </summary>
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

    /// <summary>
    /// Delete a contact from the database
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task DeleteContactAsync(string email)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Get a contact by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<Contact?> GetContactByEmailAsync(string email)
    {
        return await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
    }

    /// <summary>
    /// Get all contacts
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    /// <summary>
    /// Update a contact in the database
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    public async Task UpdateContactAsync(Contact contact)
    {
        var existingContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == contact.Email);
        if (existingContact != null)
        {
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.BirthDate = contact.BirthDate;
            existingContact.Category = contact.Category;
            existingContact.Subcategory = contact.Subcategory;
            existingContact.Password = contact.Password;
        }
        await _context.SaveChangesAsync();
    }
}
