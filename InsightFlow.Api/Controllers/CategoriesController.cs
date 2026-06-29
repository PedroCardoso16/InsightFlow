using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category is null)
            return NotFound("Categoria não encontrada.");

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
    {
        if (string.IsNullOrWhiteSpace(createCategoryDto.Name))
            return BadRequest("O nome da categoria é obrigatório.");

        var category = await _categoryService.CreateAsync(createCategoryDto);

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryDto updateCategoryDto)
    {
        if (string.IsNullOrWhiteSpace(updateCategoryDto.Name))
            return BadRequest("O nome da categoria é obrigatório.");

        var category = await _categoryService.UpdateAsync(id, updateCategoryDto);

        if (category is null)
            return NotFound("Categoria não encontrada.");

        return Ok(category);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _categoryService.DeleteAsync(id);

        if (!deleted)
            return NotFound("Categoria não encontrada.");

        return NoContent();
    }
}
