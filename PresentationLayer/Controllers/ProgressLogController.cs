//using Fitness.Entities;
//using Fitness.Entities.Dto;
//using Fitness.Entities.Models;
//using Fitness.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace Fitness.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProgressLogController : ControllerBase
//    {
//        private readonly IProgressLogService _progressLogService;

//        public ProgressLogController(IProgressLogService progressLogService)
//        {
//            _progressLogService = progressLogService;
//        }

//        [HttpGet("user/{userId}")]
//        public async Task<ActionResult<ServiceResponse<List<ProgressLogDto>>>> GetProgressLogsByUserId(int userId)
//        {
//            return Ok(await _progressLogService.GetProgressLogsByUserId(userId));
//        }

//        [HttpPost("user/{userId}")]
//        public async Task<ActionResult<ServiceResponse<string>>> AddProgressLogByUserId(int userId, ProgressLogDto newProgressLog)
//        {
//            return Ok(await _progressLogService.AddProgressLogByUserId(userId, newProgressLog));
//        }

//        [HttpPut("user/{userId}")]
//        public async Task<ActionResult<ServiceResponse<string>>> UpdateProgressLogByUserId(int userId, ProgressLogDto updatedProgressLog)
//        {
//            return Ok(await _progressLogService.UpdateProgressLogByUserId(userId, updatedProgressLog));
//        }

//        [HttpDelete("user/{userId}/{progressLogId}")]
//        public async Task<ActionResult<ServiceResponse<string>>> DeleteProgressLogByUserId(int userId, int progressLogId)
//        {
//            return Ok(await _progressLogService.DeleteProgressLogByUserId(userId, progressLogId));
//        }
//    }
//}