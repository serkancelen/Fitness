//using Fitness.Entities;
//using Fitness.Entities.Dto;
//using Fitness.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace Fitness.Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UserController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers()
//        {
//            return Ok(await _userService.GetAllUser());
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserById(int id)
//        {
//            var response = await _userService.GetUserById(id);
//            if (response.Data == null)
//            {
//                return NotFound(response);
//            }
//            return Ok(response);
//        }

//        [HttpPut]
//        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto updatedUser)
//        {
//            var response = await _userService.UpdateUser(updatedUser);
//            if (response.Data == null)
//            {
//                return NotFound(response);
//            }
//            return Ok(response);
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(int id)
//        {
//            var response = await _userService.DeleteUser(id);
//            if (!response.Success)
//            {
//                return NotFound(response);
//            }
//            return Ok(response);
//        }
//    }
//}
