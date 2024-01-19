using Fitness.Entities.Dto;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Authorize]
    [Route("api/nutrition")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionService _nutritionService;

        public NutritionController(INutritionService nutritionService)
        {
            _nutritionService = nutritionService;
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetNutritionEntries(int userId)
        {
            var entries = await _nutritionService.GetNutritionByUserIdAsync(userId);
            if (entries == null)
            {
                return NotFound("Beslenme kaydı bulunamadı.");
            }
            return Ok(entries);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNutritionEntry([FromBody] NutritionDto nutritionEntryDto)
        {
            var result = await _nutritionService.CreateNutritionAsync(nutritionEntryDto);

            if (result.Success)
            {
                return Ok("Beslenme kaydı başarıyla oluşturuldu.");
            }

            return BadRequest("Beslenme kaydı oluşturulurken bir hata oluştu.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateNutritionEntry(int id, [FromBody] NutritionDto nutritionEntryDto)
        {
            var result = await _nutritionService.UpdateNutritionAsync(id, nutritionEntryDto);

            if (result.Success)
            {
                return Ok("Beslenme kaydı başarıyla güncellendi.");
            }

            return BadRequest("Beslenme kaydı güncellenirken bir hata oluştu.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNutritionEntry(int id)
        {
            var result = await _nutritionService.DeleteNutritionAsync(id);

            if (result.Success)
            {
                return Ok("Beslenme kaydı başarıyla silindi.");
            }

            return BadRequest("Beslenme kaydı silinirken bir hata oluştu.");
        }
    }
}
