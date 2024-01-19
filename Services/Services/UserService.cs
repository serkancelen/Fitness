using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    response.Data = _context.Users.Select(x => _mapper.Map<GetUserDto>(x)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Kullanıcı Bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        [Authorize]
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUser()
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.ToListAsync();

            response.Data = dbUsers.Select(x => _mapper.Map<GetUserDto>(x)).ToList();
            return response;
        }
        [Authorize]
        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var dbUser = await _context.Users

                    .Include(u => u.ProgressLogs)

                    .FirstOrDefaultAsync(u => u.Id == id);

                if (dbUser == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Kullanıcı bulunamadı.";
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
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.Id);
                if (user != null)
                {
                    
                    if (!string.IsNullOrWhiteSpace(updatedUser.NewPassword))
                    {
                        CreatePasswordHash(updatedUser.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                        user.PasswordHash = passwordHash;
                        user.PasswordSalt = passwordSalt;
                    }

                    user.FullName = updatedUser.FullName;
                    user.Birthdate = updatedUser.Birthdate;
                    user.UserName = updatedUser.Username;
                    user.Email = updatedUser.Email;
                    user.PhoneNumber = updatedUser.PhoneNumber;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<UpdateUserDto>(user);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Kullanıcı Bulunamadı.";
                }
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
