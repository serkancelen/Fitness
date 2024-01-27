using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;

namespace Fitness.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUser();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<UpdateUserDto>> UpdateUser(UpdateUserDto updatedUser, string newPassword);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
        Task<ServiceResponse<List<User>>> GetAllUsersWithProgressLogs();
    }
}
