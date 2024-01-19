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
            var entries = await _exerciseService.GetExerciseUserIdAsync(userId);
            if (entries == null)
            {
                return NotFound("Antrenman kaydı bulunamadı.");
            }
            return Ok(entries);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateExerciseEntry([FromBody] ExerciseDto exerciseEntryDto)
        {
            var result = await _exerciseService.CreateExerciseAsync(exerciseEntryDto);

            if (result.Success)
            {
                return Ok("Antrenman kaydı başarıyla oluşturuldu.");
            }

            return BadRequest("Antrenman kaydı oluşturulurken bir hata oluştu.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateExerciseEntry(int id, [FromBody] ExerciseDto exerciseEntryDto)
        {
            var result = await _exerciseService.UpdateExerciseAsync(id, exerciseEntryDto);

            if (result.Success)
            {
                return Ok("Antrenman kaydı başarıyla güncellendi.");
            }

            return BadRequest("Antrenman kaydı güncellenirken bir hata oluştu.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExerciseEntry(int id)
        {
            var result = await _exerciseService.DeleteExerciseAsync(id);

            if (result.Success)
            {
                return Ok("Antrenman kaydı başarıyla silindi.");
            }

            return BadRequest("Antrenman kaydı silinirken bir hata oluştu.");
        }
    }
}
