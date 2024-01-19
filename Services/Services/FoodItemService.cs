using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Fitness.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class FoodItemService : IFoodItemService
{
    private readonly FitnessDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FoodItemService(FitnessDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<FoodItemDto>>> GetFoodItemsByUserIdAsync(int userId)
    {
        var response = new ServiceResponse<List<FoodItemDto>>();

        try
        {
            var requestingUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userId != requestingUserId)
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
                return response;
            }

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
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (foodItemDto.UserId != userId)
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
                return response;
            }

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

            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (existingFoodItem.UserId != userId)
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
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

            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (existingFoodItem.UserId != userId)
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
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
