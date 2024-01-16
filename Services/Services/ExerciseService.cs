using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.DataAccess;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly FitnessDbContext _context;

        public ExerciseService(FitnessDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<ExerciseDto>>> GetExerciseEntriesByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<List<ExerciseDto>>();

            try
            {
                var entries = await _context.Exercises
                    .Where(e => e.UserId == userId)
                    .ToListAsync();

                response.Data = entries.Select(entry => new ExerciseDto
                {
                    Id = entry.Id,
                    ExerciseName = entry.ExerciseName,
                    DurationMinutes = entry.DurationMinutes,
                    EntryDate = entry.EntryDate,
                    UserId = entry.UserId
                }).ToList();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<ServiceResponse<string>> CreateExerciseEntryAsync(ExerciseDto exerciseDto)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var exerciseEntry = new Exercise
                {
                    ExerciseName = exerciseDto.ExerciseName,
                    DurationMinutes = exerciseDto.DurationMinutes,
                    EntryDate = exerciseDto.EntryDate,
                    UserId = exerciseDto.UserId
                };

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
        public async Task<ServiceResponse<string>> UpdateExerciseEntryAsync(int id, ExerciseDto exerciseDto)
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

                // Güncelleme işlemleri
                existingEntry.ExerciseName = exerciseDto.ExerciseName;
                existingEntry.DurationMinutes = exerciseDto.DurationMinutes;
                existingEntry.EntryDate = exerciseDto.EntryDate;

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
        public async Task<ServiceResponse<string>> DeleteExerciseEntryAsync(int id)
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
