using InsightFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await _dashboardService.GetSummaryAsync();

        return Ok(summary);
    }

    [HttpGet("by-status")]
    public async Task<IActionResult> GetByStatus()
    {
        var data = await _dashboardService.GetByStatusAsync();

        return Ok(data);
    }

    [HttpGet("by-priority")]
    public async Task<IActionResult> GetByPriority()
    {
        var data = await _dashboardService.GetByPriorityAsync();

        return Ok(data);
    }

    [HttpGet("by-category")]
    public async Task<IActionResult> GetByCategory()
    {
        var data = await _dashboardService.GetByCategoryAsync();

        return Ok(data);
    }
}