using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Persistence.Repositories;
using ContactBook.Application.Common.Validators;
using ContactBook.Application.Common.Security;
using ContactBook.Application.Common.Exceptions;
using System.Security.Cryptography.X509Certificates;

namespace ContactBook.Application.Services.Contacts;

/// <summary>
/// Contact service
/// </summary>
public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    /// <summary>
    /// Get all contacts
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return await _contactRepository.GetContactsAsync();
    }

    /// <summary>
    /// Get contact by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<Contact?> GetContactByEmailAsync(string email)
    {

        return await _contactRepository.GetContactByEmailAsync(email);

    }

    /// <summary>
    /// Add a new contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ValidationException"></exception>
    public async Task AddContactAsync(Contact contact)
    {
        // Check if contact already exists
        var existingContact = await _contactRepository.GetContactByEmailAsync(contact.Email);
        if (existingContact != null)
        {
            throw new Exception("Contact already exists");
        }

        // Validate password
        var passwordValidCheck = PasswordValidator.ValidatePassword(contact.Password);
        if (!passwordValidCheck.IsValid)
        {
            throw new ValidationException(passwordValidCheck);
        }

        // Validate email
        var emailValidCheck = EmailValidator.ValidateEmail(contact.Email);
        if (!emailValidCheck.IsValid)
        {
            throw new ValidationException(emailValidCheck);
        }

        // Validate phone number
        var phoneNumberValidCheck = PhoneValidator.ValidatePhone(contact.PhoneNumber);
        if (!phoneNumberValidCheck.IsValid)
        {
            throw new ValidationException(phoneNumberValidCheck);
        }

        // Check if are not empty
        // firstname, lastname, category, subcategory, birthdate
        if (string.IsNullOrWhiteSpace(contact.FirstName) ||
            string.IsNullOrWhiteSpace(contact.LastName) || 
            string.IsNullOrWhiteSpace(contact.Category) || 
            string.IsNullOrWhiteSpace(contact.Subcategory) || 
            contact.BirthDate == null)
        {
            throw new Exception("Fields cannot be empty");
        }

        // Hash password
        contact.Password = PasswordHash.HashPassword(contact.Password);

        await _contactRepository.AddContactAsync(contact);
    }

    /// <summary>
    /// Update a contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ValidationException"></exception>
    public async Task UpdateContactAsync(Contact contact)
    {
        // Check if contact already exists
        var existingContact = await _contactRepository.GetContactByEmailAsync(contact.Email);
        if (existingContact == null)
        {
            throw new Exception("Contact doesn't exists");
        }

        // Validate password if
        if (!string.IsNullOrWhiteSpace(contact.Password)) {
            var passwordValidCheck = PasswordValidator.ValidatePassword(contact.Password);
            if (!passwordValidCheck.IsValid)
            {
                throw new ValidationException(passwordValidCheck);
            }
            
            // Hash password
            contact.Password = PasswordHash.HashPassword(contact.Password);
        } else {
            contact.Password = existingContact.Password;
        }

        // Validate email
        var emailValidCheck = EmailValidator.ValidateEmail(contact.Email);
        if (!emailValidCheck.IsValid)
        {
            throw new ValidationException(emailValidCheck);
        }

        // Validate phone number
        var phoneNumberValidCheck = PhoneValidator.ValidatePhone(contact.PhoneNumber);
        if (!phoneNumberValidCheck.IsValid)
        {
            throw new ValidationException(phoneNumberValidCheck);
        }

        // Check if are not empty
        // firstname, lastname, category, subcategory, birthdate
        if (string.IsNullOrWhiteSpace(contact.FirstName) ||
            string.IsNullOrWhiteSpace(contact.LastName) || 
            string.IsNullOrWhiteSpace(contact.Category) || 
            string.IsNullOrWhiteSpace(contact.Subcategory) || 
            contact.BirthDate == null)
        {
            throw new Exception("Fields cannot be empty");
        }

        await _contactRepository.UpdateContactAsync(contact);
    }

    /// <summary>
    /// Delete a contact
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task DeleteContactAsync(string email)
    {
        var existingContact = await _contactRepository.GetContactByEmailAsync(email);
        if (existingContact == null)
        {
            throw new Exception("Contact does not exist");
        }
        await _contactRepository.DeleteContactAsync(email);
    }
}