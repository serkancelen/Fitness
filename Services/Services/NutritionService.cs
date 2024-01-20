using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Fitness.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class NutritionService : INutritionService
{
    private readonly FitnessDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NutritionService(FitnessDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

    public async Task<ServiceResponse<List<NutritionDto>>> GetNutritionByUserIdAsync(int userId)
    {
        var response = new ServiceResponse<List<NutritionDto>>();

        try
        {
            var requestingUserId = GetUserId();

            if (userId != requestingUserId)
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
                return response;
            }

            var entries = await _context.Nutritions
                .Include(n => n.FoodItems)
                .Where(n => n.UserId == userId)
                .ToListAsync();

            if (entries == null || entries.Count == 0)
            {
                response.Success = false;
                response.Message = "Kullanıcıya Bağlı Beslenme Programı Bulunamadı";
                return response;
            }

            response.Data = _mapper.Map<List<NutritionDto>>(entries);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }


    public async Task<ServiceResponse<string>> CreateNutritionAsync(NutritionDto nutritionDto)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var userId = GetUserId();
            if (nutritionDto.UserId != userId)
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
                return response;
            }

            var nutritionEntry = _mapper.Map<Nutrition>(nutritionDto);
            await _context.Nutritions.AddAsync(nutritionEntry);
            await _context.SaveChangesAsync();
            response.Data = nutritionEntry.Id.ToString() + "Beslenme kaydı başarıyla oluşturuldu.";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<string>> UpdateNutritionAsync(int nutritionId, NutritionDto updatedDto)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var existingEntry = await _context.Nutritions
                .Include(n => n.FoodItems)
                .FirstOrDefaultAsync(n => n.Id == nutritionId);

            if (existingEntry == null)
            {
                response.Success = false;
                response.Message = "Belirtilen beslenme girişi bulunamadı.";
                return response;
            }


            if (existingEntry.UserId != GetUserId())
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
                return response;
            }

            _mapper.Map(updatedDto, existingEntry);

            UpdateNutrition(existingEntry.FoodItems, updatedDto.FoodItems);

            await _context.SaveChangesAsync();

            response.Data = nutritionId.ToString() + "Belirtilen beslenme başarıyla güncellendi.";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
    private void UpdateNutrition(List<FoodItem> existingFoodItems, List<FoodItemDto> updatedFoodItems)
    {
        var foodToRemove = existingFoodItems.Where(f => updatedFoodItems.All(updated => updated.FoodItemId != f.FoodItemId)).ToList();
        foreach (var foodItem in foodToRemove)
        {
            existingFoodItems.Remove(foodItem);
        }
        foreach (var updatedItem in updatedFoodItems)
        {
            var existingFoodItem = existingFoodItems.FirstOrDefault(f => f.FoodItemId == updatedItem.FoodItemId);
            if (existingFoodItem != null)
            {
                _mapper.Map(updatedItem, existingFoodItem);
            }
            else
            {
                var newFoodItem = _mapper.Map<FoodItem>(updatedItem);
                existingFoodItems.Add(newFoodItem);
            }
        }
    }

    public async Task<ServiceResponse<string>> DeleteNutritionAsync(int nutritionId)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var existingEntry = await _context.Nutritions
                .Include(n => n.FoodItems)
                .FirstOrDefaultAsync(n => n.Id == nutritionId);

            if (existingEntry == null)
            {
                response.Success = false;
                response.Message = "Belirtilen beslenme girişi bulunamadı.";
                return response;
            }

            if (existingEntry.UserId != GetUserId())
            {
                response.Success = false;
                response.Message = "Bu işlem için yetkiniz yok.";
                return response;
            }
            _context.FoodItems.RemoveRange(existingEntry.FoodItems);
            _context.Nutritions.Remove(existingEntry); 
            await _context.SaveChangesAsync();
            response.Data = existingEntry.Id.ToString() + "Belirtilen Beslenme Başarıyla Silindi";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
