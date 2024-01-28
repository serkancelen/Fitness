using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.RequestFeatures;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Fitness.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "5mins")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers([FromQuery]FitnessParameters fitnessParameters)
        {
            Log.Information("GetAllUsers method called.");
            return Ok(await _userService.GetAllUser(fitnessParameters));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            Log.Information($"GetUserById method called with id: {id}");

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto updatedUser, string newPassword)
        {
            var response = await _userService.UpdateUser(updatedUser, newPassword);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            Log.Information($"UpdateUser method called for user id: {updatedUser.Id}");

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            Log.Information($"DeleteUser method called for user id: {id}");

            return Ok(response);
        }
    }
}
