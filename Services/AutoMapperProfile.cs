using AutoMapper;
using Fitness.Entities;
using Fitness.Entities.Dto;
using Fitness.Entities.Dto.UserDto;
using Fitness.Entities.Models;

namespace Fitness.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUserDto, User>();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<NutritionDto, Nutrition>().ReverseMap();
            CreateMap<ProgressLogDto, ProgressLog>();
            CreateMap<ProgressLog, ProgressLogDto>();
            CreateMap<WorkoutDto, Workout>().ReverseMap();
            CreateMap<ExerciseDto, Exercise>().ReverseMap();
            CreateMap<FoodItem, FoodItemDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
        }
    }
}