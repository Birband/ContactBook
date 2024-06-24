namespace ContactBook.Domain.Entities;

public class Subcategory
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}