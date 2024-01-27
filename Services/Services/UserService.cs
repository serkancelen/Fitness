using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fitness.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IMapper mapper, FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var userId = GetUserId();
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı Bulunamadı.";
                    return response;
                }

                if (user.Id != userId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                response.Data = _context.Users.Select(x => _mapper.Map<GetUserDto>(x)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUser()
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var userId = GetUserId();
                var dbUsers = await _context.Users
                    .Where(u => u.Id == userId)
                    .Include(u =>u.ProgressLogs)
                    .ToListAsync();

                response.Data = dbUsers.Select(x => _mapper.Map<GetUserDto>(x)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                var userId = GetUserId();
                var dbUser = await _context.Users
                    .Include(u => u.ProgressLogs)
                    .FirstOrDefaultAsync(u => u.Id == id && u.Id == userId);

                if (dbUser == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Bu İşlem İçin Yetkiniz Bulunmamaktadır.";
                    return serviceResponse;
                }

                serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UpdateUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<UpdateUserDto> serviceResponse = new ServiceResponse<UpdateUserDto>();
            try
            {
                var userId = GetUserId();
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.Id);

                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Kullanıcı Bulunamadı.";
                    return serviceResponse;
                }

                // Sadece kullanıcı kendi verilerini güncelleyebilir
                if (user.Id != userId)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Bu işlem için yetkiniz yok.";
                    return serviceResponse;
                }

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<UpdateUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsersWithProgressLogs()
        {
            var response = new ServiceResponse<List<User>>();
            var usersWithProgressLogs = await _context.Users.Include(u => u.ProgressLogs).ToListAsync();
            response.Data = usersWithProgressLogs;
            return response;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
    }
}
