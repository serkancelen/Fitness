using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.RequestFeatures;
using System.Security.Claims;

namespace Fitness.Services
{
    public interface IWorkoutService
    {
        Task<ServiceResponse<List<WorkoutDto>>> GetAllWorkoutsAsync(ClaimsPrincipal user);
        Task<ServiceResponse<List<WorkoutDto>>> GetAllWorkoutsByUserIdAsync(int userId, ClaimsPrincipal user);
        Task<ServiceResponse<WorkoutDto>> CreateWorkoutAsync(WorkoutDto workoutDto);
        Task<ServiceResponse<WorkoutDto>> UpdateWorkoutAsync(int id, WorkoutDto workoutDto);
        Task<ServiceResponse<string>> DeleteWorkoutAsync(int id);
    }
}
