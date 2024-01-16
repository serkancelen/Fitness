//using Fitness.DataAccess;
//using Fitness.Entities;
//using Fitness.Entities.Dto;
//using Fitness.Entities.Dto.UserDto;
//using Fitness.Entities.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace Fitness.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        public IAuthRepository _authRepo;
//        public AuthController(IAuthRepository authRepo)
//        {
//            _authRepo = authRepo;
//        }
//        [HttpPost("register")]
//        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
//        {
//            var response = await _authRepo.Register(
//                new User { UserName = request.UserName }, request.Password); //request.FullName);
//            if (!response.Success)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//        [HttpPost("login")]
//        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
//        {
//            var response = await _authRepo.Login(request.UserName, request.Password);

//            if (!response.Success)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//    }

//}
