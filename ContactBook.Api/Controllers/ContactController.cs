using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Application.Services.Contacts;
using ContactBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers;

/// <summary>
/// Controller for managing contacts
/// </summary>
/// <response code="401">If the user is not authenticated</response>
[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public ContactController(IContactService contactService, IMapper mapper)
    {
        _contactService = contactService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all contacts from the database
    /// </summary>
    /// <response code="200">Returns all contacts</response>
    [HttpGet("all")]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await _contactService.GetContactsAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Get a contact by email 
    /// </summary>
    /// <param name="email"></param>
    /// <response code="200">Returns the contact</response>
    [HttpGet("{email}")]
    public async Task<IActionResult> GetContactByEmail(string email)
    {
        var contact = await _contactService.GetContactByEmailAsync(email);
        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }

    /// <summary>
    /// Add a new contact
    /// </summary>
    /// <param name="contact"></param>
    /// <response code="200">If the contact was added successfully</response>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddContact([FromBody] CreateContactDto contact)
    {
        await _contactService.AddContactAsync(_mapper.Map<Contact>(contact));
        return Ok();
    }

    /// <summary>
    /// Update a contact in the database
    /// </summary>
    /// <param name="contact"></param>
    /// <response code="200">If the contact was updated successfully</response>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateContact([FromBody] CreateContactDto contact)
    {
        await _contactService.UpdateContactAsync(_mapper.Map<Contact>(contact));
        return Ok();
    }

    /// <summary>
    /// Delete a contact by email from the database
    /// </summary>
    /// <param name="email"></param>
    /// <response code="200">If the contact was deleted successfully</response>
    [HttpDelete("{email}")]
    [Authorize]
    public async Task<IActionResult> DeleteContact(string email)
    {
        await _contactService.DeleteContactAsync(email);
        return Ok();
    }
}
