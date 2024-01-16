using Fitness.Entities.Dto;
using Fitness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Services
{
    public interface IFoodItemService
    {
        Task<ServiceResponse<List<FoodItemDto>>> GetFoodItemsByUserIdAsync(int userId);
        Task<ServiceResponse<string>> CreateFoodItemAsync(FoodItemDto foodItemDto);
        Task<ServiceResponse<string>> UpdateFoodItemAsync(int id, FoodItemDto foodItemDto);
        Task<ServiceResponse<string>> DeleteFoodItemAsync(int id);
    }

}
