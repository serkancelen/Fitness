using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            try
            {
                if (await _authRepo.UserExists(request.UserName))
                {
                    Log.Information($"Register failed - UserExists: {request.UserName}");
                    return BadRequest(new ServiceResponse<int> { Message = "Bu kullanıcı adı zaten kullanılmaktadır." });
                }

                var user = new User
                {
                    UserName = request.UserName,
                    FullName = request.FullName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Gender = request.Gender,
                    Birthdate = request.Birthdate,
                };

                var response = await _authRepo.Register(user, request.Password);

                if (!response.Success)
                {
                    Log.Error($"Register failed: {response.Message}");
                    return BadRequest(response);
                }

                Log.Information($"User registered successfully: {user.UserName}");
                return Ok(new { UserId = response.Data, Message = "Kullanıcı kaydı başarıyla tamamlandı." });
            }
            catch (Exception ex)
            {
                Log.Error($"Register failed with exception: {ex.Message}");
                return BadRequest(new ServiceResponse<int> { Message = $"Kayıt işlemi sırasında bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(request.UserName, request.Password);

            if (!response.Success)
            {
                Log.Error($"Login failed: {response.Message}");
                return BadRequest(response);
            }

            Log.Information($"User logged in successfully: {request.UserName}");
            return Ok(response);
        }
    }
}
