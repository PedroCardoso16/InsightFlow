using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemandsController : ControllerBase
{
    private readonly IDemandService _demandService;

    public DemandsController(IDemandService demandService)
    {
        _demandService = demandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var demands = await _demandService.GetAllAsync();

        return Ok(demands);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var demand = await _demandService.GetByIdAsync(id);

        if (demand is null)
            return NotFound("Demanda não encontrada.");

        return Ok(demand);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDemandDto createDemandDto)
    {
        if (string.IsNullOrWhiteSpace(createDemandDto.Title))
            return BadRequest("O título da demanda é obrigatório.");

        if (string.IsNullOrWhiteSpace(createDemandDto.Description))
            return BadRequest("A descrição da demanda é obrigatória.");

        try
        {
            var demand = await _demandService.CreateAsync(createDemandDto);

            return CreatedAtAction(nameof(GetById), new { id = demand.Id }, demand);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateDemandDto updateDemandDto)
    {
        if (string.IsNullOrWhiteSpace(updateDemandDto.Title))
            return BadRequest("O título da demanda é obrigatório.");

        if (string.IsNullOrWhiteSpace(updateDemandDto.Description))
            return BadRequest("A descrição da demanda é obrigatória.");

        try
        {
            var demand = await _demandService.UpdateAsync(id, updateDemandDto);

            if (demand is null)
                return NotFound("Demanda não encontrada.");

            return Ok(demand);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateDemandStatusDto updateDemandStatusDto)
    {
        var demand = await _demandService.UpdateStatusAsync(id, updateDemandStatusDto);

        if (demand is null)
            return NotFound("Demanda não encontrada.");

        return Ok(demand);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _demandService.DeleteAsync(id);

        if (!deleted)
            return NotFound("Demanda não encontrada.");

        return NoContent();
    }
}