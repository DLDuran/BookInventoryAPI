using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookInventory.Application.Interfaces.Services;

namespace BookInventory.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatisticsController : ApiControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetUserSummary()
    {
        var books = await _statisticsService.GetUserSummaryAsync(CurrentUserId);
        return Ok(books);
    }
}