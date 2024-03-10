using AspNetCoreRateLimit;
using Fitness.DataAccess;
using Fitness.Extensions;
using Fitness.Services;
using Fitness.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Configuration dosyasýný yükle
builder.Configuration.AddJsonFile("appsettings.json");

// Veritabaný baðlantýsý ve diðer servisler
builder.Services.ConfigureSqlContext(builder.Configuration);
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
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureResponseCaching();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.ConfigureServiceRegistration();

// AutoMapper konfigürasyonu
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(AutoMapperProfile).Assembly);

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
