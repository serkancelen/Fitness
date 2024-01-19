using AutoMapper;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fitness.Services.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly FitnessDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkoutService(FitnessDbContext context, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<WorkoutDto>> CreateWorkoutAsync(WorkoutDto workoutDto)
        {
            var response = new ServiceResponse<WorkoutDto>();

            try
            {
                var userId = GetUserId();
                var workout = _mapper.Map<Workout>(workoutDto);

                var user = await _context.Users.FindAsync(workoutDto.UserId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı bulunamadı.";
                    return response;
                }

                if (workoutDto.UserId != userId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                workout.User = user;

                workout.Exercises = workoutDto.Exercises?.Select(exerciseDto =>
                {
                    var exercise = _mapper.Map<Exercise>(exerciseDto);
                    exercise.UserId = workoutDto.UserId; 
                    return exercise;
                }).ToList() ?? new List<Exercise>();

                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<WorkoutDto>(workout);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<WorkoutDto>> UpdateWorkoutAsync(int id, WorkoutDto workoutDto)
        {
            var response = new ServiceResponse<WorkoutDto>();

            try
            {
                var existingWorkout = await _context.Workouts
                    .Include(w => w.Exercises)
                    .FirstOrDefaultAsync(w => w.WorkoutId == id);

                if (existingWorkout == null)
                {
                    response.Success = false;
                    response.Message = "Antrenman bulunamadı.";
                    return response;
                }

                if (existingWorkout.UserId != GetUserId())
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                _mapper.Map(workoutDto, existingWorkout);

                UpdateExercises(existingWorkout.Exercises, workoutDto.Exercises);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<WorkoutDto>(existingWorkout);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        private void UpdateExercises(List<Exercise> existingExercises, List<ExerciseDto> updatedExercises)
        {
            var exercisesToRemove = existingExercises.Where(e => updatedExercises.All(updated => updated.Id != e.Id)).ToList();
            foreach (var exerciseToRemove in exercisesToRemove)
            {
                existingExercises.Remove(exerciseToRemove);
            }

            foreach (var updatedExerciseDto in updatedExercises)
            {
                var existingExercise = existingExercises.FirstOrDefault(e => e.Id == updatedExerciseDto.Id);
                if (existingExercise != null)
                {
                    _mapper.Map(updatedExerciseDto, existingExercise);
                }
                else
                {
                    var newExercise = _mapper.Map<Exercise>(updatedExerciseDto);
                    existingExercises.Add(newExercise);
                }
            }
        }
        public async Task<ServiceResponse<string>> DeleteWorkoutAsync(int id)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var workout = await _context.Workouts
                    .Include(w => w.Exercises) 
                    .FirstOrDefaultAsync(w => w.WorkoutId == id);

                if (workout == null)
                {
                    response.Success = false;
                    response.Message = "Antrenman bulunamadı.";
                    return response;
                }

                if (workout.UserId != GetUserId())
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                _context.Exercises.RemoveRange(workout.Exercises);

                _context.Workouts.Remove(workout);

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Antrenman başarıyla silindi.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<ServiceResponse<List<WorkoutDto>>> GetAllWorkoutsAsync(ClaimsPrincipal user)
        {
            var response = new ServiceResponse<List<WorkoutDto>>();

            try
            {
                var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var workouts = await _context.Workouts
                    .Include(w => w.Exercises)
                    .Where(w => w.UserId == userId)
                    .ToListAsync();

                response.Data = _mapper.Map<List<WorkoutDto>>(workouts);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<ServiceResponse<List<WorkoutDto>>> GetAllWorkoutsByUserIdAsync(int userId, ClaimsPrincipal user)
        {
            var response = new ServiceResponse<List<WorkoutDto>>();

            try
            {
                if (userId != int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var workouts = await _context.Workouts
                    .Include(w => w.Exercises)
                    .Where(w => w.UserId == userId)
                    .ToListAsync();

                if (workouts == null || workouts.Count == 0)
                {
                    response.Success = false;
                    response.Message = "Kullanıcının antrenmanları bulunamadı.";
                    return response;
                }

                response.Data = _mapper.Map<List<WorkoutDto>>(workouts);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


    }
}
