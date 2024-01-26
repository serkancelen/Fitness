using Fitness.DataAccess;
using Fitness.Services;
using Fitness.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Configuration dosyasýný yükle
builder.Configuration.AddJsonFile("appsettings.json");

// Veritabaný baðlantýsý ve diðer servisler
builder.Services.AddDbContext<FitnessDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("5mins", new CacheProfile
    {
        Duration = 300,  
        Location = ResponseCacheLocation.Client,  
    });
})
.AddApplicationPart(typeof(Fitness.Presentation.AssemblyReferance).Assembly);


// Swagger/OpenAPI konfigürasyonu
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer Ön Ekini Kullan \"bearer {token} \"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    x.OperationFilter<SecurityRequirementsOperationFilter>();
});
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/myBeautifulLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Services.AddResponseCaching();
builder.Services.AddHttpCacheHeaders();
builder.Services.AddMemoryCache();

var rateLimitRules = new List<RateLimitRule>
{
    new RateLimitRule
    {
        Endpoint = "*",
        Limit = 3,
        Period = "1m"
    }
};

builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = rateLimitRules;
});
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddHttpContextAccessor();

// AutoMapper konfigürasyonu
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(AutoMapperProfile).Assembly);

// DI (Dependency Injection) konfigürasyonlarý
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<INutritionService, NutritionService>();
builder.Services.AddScoped<IProgressLogService, ProgressLogService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
