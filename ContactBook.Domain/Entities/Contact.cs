using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Category { get; set; }
    public string? Subcategory { get; set; }
}