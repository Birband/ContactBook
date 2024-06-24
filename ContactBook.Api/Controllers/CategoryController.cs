using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Application.Services.Categories;
using ContactBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
    {
        await _categoryService.AddCategoryAsync(_mapper.Map<Category>(category));
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category)
    {
        await _categoryService.UpdateCategoryAsync(_mapper.Map<Category>(category));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return Ok();
    }
}