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
    public class ProgressLogService : IProgressLogService
    {
        private readonly FitnessDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProgressLogService(FitnessDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<ProgressLogDto>>> GetProgressLogsByUserId(int userId)
        {
            var response = new ServiceResponse<List<ProgressLogDto>>();
            try
            {
               

                if (userId != int.Parse(U)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var progressLogs = await _context.ProgressLogs
                    .Where(pl => pl.UserId == userId)
                    .ToListAsync();

                var progressLogsDto = _mapper.Map<List<ProgressLogDto>>(progressLogs);
                response.Data = progressLogsDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> AddProgressLogByUserId(int userId, ProgressLogDto newProgressLog)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var requestingUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId != requestingUserId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı bulunamadı.";
                    return response;
                }

                var progressLog = _mapper.Map<ProgressLog>(newProgressLog);
                progressLog.UserId = userId;

                _context.ProgressLogs.Add(progressLog);
                await _context.SaveChangesAsync();

                response.Data = "Progress log başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateProgressLogByUserId(int userId, ProgressLogDto updatedProgressLog)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var requestingUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId != requestingUserId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı bulunamadı.";
                    return response;
                }

                var existingProgressLog = await _context.ProgressLogs.FindAsync(updatedProgressLog.Id);
                if (existingProgressLog == null || existingProgressLog.UserId != userId)
                {
                    response.Success = false;
                    response.Message = "Progress log bulunamadı veya kullanıcıya ait değil.";
                    return response;
                }

                _mapper.Map(updatedProgressLog, existingProgressLog);

                await _context.SaveChangesAsync();

                response.Data = "Progress log başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteProgressLogByUserId(int userId, int progressLogId)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var requestingUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId != requestingUserId)
                {
                    response.Success = false;
                    response.Message = "Bu işlem için yetkiniz yok.";
                    return response;
                }

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı bulunamadı.";
                    return response;
                }

                var progressLog = await _context.ProgressLogs.FindAsync(progressLogId);
                if (progressLog == null || progressLog.UserId != userId)
                {
                    response.Success = false;
                    response.Message = "Progress log bulunamadı veya kullanıcıya ait değil.";
                    return response;
                }

                _context.ProgressLogs.Remove(progressLog);
                await _context.SaveChangesAsync();

                response.Data = "Progress log başarıyla silindi.";
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
