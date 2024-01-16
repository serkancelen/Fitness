using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Fitness.Services;
using Microsoft.EntityFrameworkCore;

public class FoodItemService : IFoodItemService
{
    private readonly FitnessDbContext _context;
    private readonly IMapper _mapper;

    public FoodItemService(FitnessDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<List<FoodItemDto>>> GetFoodItemsByUserIdAsync(int userId)
    {
        var response = new ServiceResponse<List<FoodItemDto>>();

        try
        {
            var foodItems = await _context.FoodItems
                .Where(f => f.UserId == userId)
                .ToListAsync();

            response.Data = _mapper.Map<List<FoodItemDto>>(foodItems);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<string>> CreateFoodItemAsync(FoodItemDto foodItemDto)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var foodItem = _mapper.Map<FoodItem>(foodItemDto);
            await _context.FoodItems.AddAsync(foodItem);
            await _context.SaveChangesAsync();
            response.Data = foodItem.FoodItemId.ToString();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<string>> UpdateFoodItemAsync(int id, FoodItemDto foodItemDto)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var existingFoodItem = await _context.FoodItems.FindAsync(id);

            if (existingFoodItem == null)
            {
                response.Message = "Belirtilen besin bulunamadı.";
                return response;
            }

            _mapper.Map(foodItemDto, existingFoodItem);

            _context.FoodItems.Update(existingFoodItem);
            await _context.SaveChangesAsync();

            response.Data = "Belirtilen Besin Güncellendi";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<string>> DeleteFoodItemAsync(int id)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var existingFoodItem = await _context.FoodItems.FindAsync(id);

            if (existingFoodItem == null)
            {
                response.Message = "Belirtilen besin bulunamadı.";
                return response;
            }

            _context.FoodItems.Remove(existingFoodItem);
            await _context.SaveChangesAsync();

            response.Data = id.ToString();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
