using System.Collections.Generic;
using System.Threading.Tasks;
using Fitness.Entities;
using Fitness.Entities.Dto;

namespace Fitness.Services
{
    public interface IExerciseService
    {
        Task<ServiceResponse<List<ExerciseDto>>> GetExerciseEntriesByUserIdAsync(int userId);
        Task<ServiceResponse<string>> CreateExerciseEntryAsync(ExerciseDto exerciseDto);
        Task<ServiceResponse<string>> UpdateExerciseEntryAsync(int id, ExerciseDto exerciseDto);
        Task<ServiceResponse<string>> DeleteExerciseEntryAsync(int id);
    }
}
