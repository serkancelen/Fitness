using Fitness.Entities.Dto;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;
        public FoodItemController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFoodItemsByUserId(int userId)
        {
            var response = await _foodItemService.GetFoodItemsByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            var response = await _foodItemService.CreateFoodItemAsync(foodItemDto);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateFoodItem(int id, [FromBody] FoodItemDto foodItemDto)
        {
            var response = await _foodItemService.UpdateFoodItemAsync(id, foodItemDto);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var response = await _foodItemService.DeleteFoodItemAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
    }
}
