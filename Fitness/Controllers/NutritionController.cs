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
            var response = await _nutritionService.GetNutritionByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNutritionEntry([FromBody] NutritionDto nutritionEntryDto)
        {
            var response = await _nutritionService.CreateNutritionAsync(nutritionEntryDto);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateNutritionEntry(int id, [FromBody] NutritionDto nutritionEntryDto)
        {
            var response = await _nutritionService.UpdateNutritionAsync(id, nutritionEntryDto);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNutritionEntry(int id)
        {
            var response = await _nutritionService.DeleteNutritionAsync(id);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }
    }
}
