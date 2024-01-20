using Fitness.Entities.Dto;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Authorize]
    [Route("api/exercise")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetExerciseEntries(int userId)
        {
            var response = await _exerciseService.GetExerciseUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateExerciseEntry([FromBody] ExerciseDto exerciseEntryDto)
        {
            var response = await _exerciseService.CreateExerciseAsync(exerciseEntryDto);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateExerciseEntry(int id, [FromBody] ExerciseDto exerciseEntryDto)
        {
            var response = await _exerciseService.UpdateExerciseAsync(id, exerciseEntryDto);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExerciseEntry(int id)
        {
            var response = await _exerciseService.DeleteExerciseAsync(id);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }
    }
}
