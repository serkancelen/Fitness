using Fitness.Entities.Dto;
using Fitness.Entities;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Authorize]
[Route("api/[controller]")]
[ApiController]
[ResponseCache(CacheProfileName = "5mins")]
public class ProgressLogController : ControllerBase
{
    private readonly IProgressLogService _progressLogService;

    public ProgressLogController(IProgressLogService progressLogService)
    {
        _progressLogService = progressLogService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ServiceResponse<List<ProgressLogDto>>>> GetProgressLogsByUserId(int userId)
    {
        var response = await _progressLogService.GetProgressLogsByUserId(userId);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"GetProgressLogsByUserId failed for UserId {userId}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpPost("user/{userId}")]
    public async Task<ActionResult<ServiceResponse<string>>> AddProgressLogByUserId(int userId, ProgressLogDto newProgressLog)
    {
        var response = await _progressLogService.AddProgressLogByUserId(userId, newProgressLog);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"AddProgressLogByUserId failed for UserId {userId}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpPut("user/{userId}")]
    public async Task<ActionResult<ServiceResponse<string>>> UpdateProgressLogByUserId(int userId, ProgressLogDto updatedProgressLog)
    {
        var response = await _progressLogService.UpdateProgressLogByUserId(userId, updatedProgressLog);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"UpdateProgressLogByUserId failed for UserId {userId}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpDelete("user/{userId}/{progressLogId}")]
    public async Task<ActionResult<ServiceResponse<string>>> DeleteProgressLogByUserId(int userId, int progressLogId)
    {
        var response = await _progressLogService.DeleteProgressLogByUserId(userId, progressLogId);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"DeleteProgressLogByUserId failed for UserId {userId}, ProgressLogId {progressLogId}: {response.Message}");

        return BadRequest(response.Message);
    }
}
