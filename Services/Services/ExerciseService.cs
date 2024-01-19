using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fitness.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ExerciseService(FitnessDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ExerciseDto>>> GetExerciseUserIdAsync(int userId)
        {
            var response = new ServiceResponse<List<ExerciseDto>>();

            try
            {
                var requestingUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId != requestingUserId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var entries = await _context.Exercises
                    .Where(e => e.UserId == userId)
                    .ToListAsync();

                response.Data = _mapper.Map<List<ExerciseDto>>(entries);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> CreateExerciseAsync(ExerciseDto exerciseDto)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (exerciseDto.UserId != userId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var exerciseEntry = _mapper.Map<Exercise>(exerciseDto);

                await _context.Exercises.AddAsync(exerciseEntry);
                await _context.SaveChangesAsync();
                response.Data = exerciseEntry.Id.ToString();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> UpdateExerciseAsync(int id, ExerciseDto exerciseDto)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var existingEntry = await _context.Exercises.FindAsync(id);

                if (existingEntry == null)
                {
                    response.Message = "Belirtilen egzersiz girişi bulunamadı.";
                    return response;
                }

                var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (existingEntry.UserId != userId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                _mapper.Map(exerciseDto, existingEntry);

                _context.Exercises.Update(existingEntry);
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

        public async Task<ServiceResponse<string>> DeleteExerciseAsync(int id)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var existingEntry = await _context.Exercises.FindAsync(id);

                if (existingEntry == null)
                {
                    response.Message = "Belirtilen egzersiz girişi bulunamadı.";
                    return response;
                }

                var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (existingEntry.UserId != userId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                _context.Exercises.Remove(existingEntry);
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
}
