using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Models;

namespace Fitness.Services
{
    public interface IProgressLogService
    {
        Task<ServiceResponse<List<ProgressLogDto>>> GetProgressLogsByUserId(int userId);
        Task<ServiceResponse<string>> AddProgressLogByUserId(int userId, ProgressLogDto newProgressLog);
        Task<ServiceResponse<string>> UpdateProgressLogByUserId(int userId, ProgressLogDto updatedProgressLog);
        Task<ServiceResponse<string>> DeleteProgressLogByUserId(int userId, int progressLogId);
    }
}
