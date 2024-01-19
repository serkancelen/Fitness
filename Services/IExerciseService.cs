using System.Collections.Generic;
using System.Threading.Tasks;
using Fitness.Entities;
using Fitness.Entities.Dto;

namespace Fitness.Services
{
    public interface IExerciseService
    {
        Task<ServiceResponse<List<ExerciseDto>>> GetExerciseUserIdAsync(int userId);
        Task<ServiceResponse<string>> CreateExerciseAsync(ExerciseDto exerciseDto);
        Task<ServiceResponse<string>> UpdateExerciseAsync(int id, ExerciseDto exerciseDto);
        Task<ServiceResponse<string>> DeleteExerciseAsync(int id);
    }
}
