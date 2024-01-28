using System.Collections.Generic;
using System.Threading.Tasks;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.RequestFeatures;

namespace Fitness.Services
{
    public interface INutritionService
    {
        Task<ServiceResponse<List<NutritionDto>>> GetNutritionByUserIdAsync(FitnessParameters fitnessParameters,int userId);
        Task<ServiceResponse<string>> CreateNutritionAsync(NutritionDto nutritionEntryDto);
        Task<ServiceResponse<string>> UpdateNutritionAsync(int nutritionEntryId, NutritionDto updatedNutritionEntryDto);
        Task<ServiceResponse<string>> DeleteNutritionAsync(int nutritionEntryId);
        
    }
}
