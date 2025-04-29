using WeatherSample.ApiService;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using WeatherSample.ApiService.Repositories;
using WeatherSample.ApiService.Repositories.Interfaces;

// Register Mapster mappings
MappingConfig.RegisterMappings();

var builder = WebApplication.CreateBuilder(args);

// Add EF Core with SQL Server
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

// Enabled Fluent validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WeatherForecast API",
        Description = "An API to manage weather forecasts",
        Contact = new OpenApiContact
        {
            Name = "Jayanta Konjengbam",
            Email = "jayanta.konjengbam@cempaas.com",
            Url = new Uri("https://cempaas.com")
        }
    });
});


// Add feature management
builder.Services.AddFeatureManagement();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default to version 1.0
    options.ReportApiVersions = true; // Include version information in responses
});

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast API v1");
        options.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseAuthorization();
app.MapControllers(); // Enable controller routing
app.MapDefaultEndpoints();

app.Run();
