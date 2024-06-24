using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Application.Services.Contacts;
using ContactBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers;

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

    [HttpGet("all")]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await _contactService.GetContactsAsync();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(Guid id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactDto contact)
    {
        await _contactService.AddContactAsync(_mapper.Map<Contact>(contact));
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] ContactDto contact)
    {
        await _contactService.UpdateContactAsync(_mapper.Map<Contact>(contact));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        await _contactService.DeleteContactAsync(id);
        return Ok();
    }
}
