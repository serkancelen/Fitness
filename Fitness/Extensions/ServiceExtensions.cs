using AspNetCoreRateLimit;
using Fitness.DataAccess;
using Fitness.Services.Services;
using Fitness.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fitness.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<FitnessDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
		}
		public static void ConfigureRateLimitingOptions(this IServiceCollection services)
		{
			var rateLimitRules = new List<RateLimitRule>()
			{
				new RateLimitRule()
				{
					Endpoint = "*",
					Limit = 3,
					Period = "1m"
				}
			};

			services.Configure<IpRateLimitOptions>(opt =>
			{
				opt.GeneralRules = rateLimitRules;
			});
			services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
			services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
			services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
			services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
		}
		public static void ConfigureResponseCaching(this IServiceCollection services) =>
			services.AddResponseCaching();
		public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
		}
		public static void ConfigureServiceRegistration(this IServiceCollection services)
		{
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<INutritionService, NutritionService>();
            services.AddScoped<IProgressLogService, ProgressLogService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IFoodItemService, FoodItemService>();
        }


    }
}