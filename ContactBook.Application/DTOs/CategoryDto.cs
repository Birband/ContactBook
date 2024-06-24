namespace ContactBook.Application.DTOs;

public class CategoryDto
{
    public string? Name { get; set; }
    public List<SubcategoryDto>? Subcategories { get; set; }
}