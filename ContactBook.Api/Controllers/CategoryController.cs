using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Application.Services.Categories;
using ContactBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers;

/// <summary>
/// Controller for managing categories
/// </summary>
/// <response code="401">If the user is not authenticated</response>
[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _hostEnvironment;

    public CategoryController(ICategoryService categoryService, IMapper mapper, IHostEnvironment hostEnvironment)
    {
        _categoryService = categoryService;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    [HttpGet("all")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }


    /// <summary>
    /// Add a new category
    /// </summary>
    /// <param name="category"></param>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
    {
        if (!_hostEnvironment.IsDevelopment()) return NotFound();
        await _categoryService.AddCategoryAsync(_mapper.Map<Category>(category));
        return Ok();
    }


    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="category"></param>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category)
    {
        if (!_hostEnvironment.IsDevelopment()) return NotFound();
        await _categoryService.UpdateCategoryAsync(_mapper.Map<Category>(category));
        return Ok();
    }

    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        if (!_hostEnvironment.IsDevelopment()) return NotFound();
        await _categoryService.DeleteCategoryAsync(id);
        return Ok();
    }
}