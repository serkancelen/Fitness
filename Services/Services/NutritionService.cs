using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Fitness.Services;
using Microsoft.EntityFrameworkCore;

public class NutritionService : INutritionService
{
    private readonly FitnessDbContext _context;
    private readonly IMapper _mapper;

    public NutritionService(FitnessDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<List<NutritionDto>>> GetNutritionByUserIdAsync(int userId)
    {
        var response = new ServiceResponse<List<NutritionDto>>();

        try
        {
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
    //ercan
    public async Task<ServiceResponse<string>> CreateNutritionAsync(NutritionDto nutritionDto)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var nutritionEntry = _mapper.Map<Nutrition>(nutritionDto);
            await _context.Nutritions.AddAsync(nutritionEntry);
            await _context.SaveChangesAsync();
            response.Data = nutritionEntry.Id.ToString();
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
            var existingEntry = await _context.Nutritions.FindAsync(nutritionId);

            if (existingEntry == null)
            {
                response.Message = "Belirtilen beslenme girişi bulunamadı.";
                return response;
            }

            // Güncelleme işlemleri
            existingEntry.NutritionName = updatedDto.NutritionName;
            existingEntry.Calories = updatedDto.Calories;
            existingEntry.EntryDate = updatedDto.LogDate;
            existingEntry.NutritionType = updatedDto.NutritionType;

            _context.Nutritions.Update(existingEntry);
            await _context.SaveChangesAsync();

            response.Data = nutritionId.ToString();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
    public async Task<ServiceResponse<string>> DeleteNutritionAsync(int nutritionId)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var existingEntry = await _context.Nutritions.FirstOrDefaultAsync(e => e.Id == nutritionId);

            if (existingEntry == null)
            {
                response.Message = "Belirtilen beslenme girişi bulunamadı.";
                return response;
            }

            _context.Nutritions.Remove(existingEntry); // Veritabanından silme işlemi
            await _context.SaveChangesAsync();
            response.Data = existingEntry.Id.ToString();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}