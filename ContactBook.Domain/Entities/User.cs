using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBook.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
