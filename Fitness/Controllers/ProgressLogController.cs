using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
                return Ok (response.Data);
            }
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
            return BadRequest(response.Message);
        }
    }
}