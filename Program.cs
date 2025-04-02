using Microsoft.EntityFrameworkCore;
using SmartScaleApi.Domain.Interfaces;
using SmartScaleApi.Infrastructure.Data;
using Microsoft.Extensions.AI;
using SmartScaleApi.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));
builder.Services.AddScoped<ICustomUnitService, CustomUnitService>();
builder.Services.AddScoped<ICustomUnitRepository, CustomUnitRepository>();

// Register the OllamaChatClient
builder.Services.AddSingleton(new OllamaChatClient(new Uri("http://localhost:10000/"), "qwen2.5:latest"));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder
            .AllowAnyOrigin() // Replace with the actual URL of your Angular app
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
// Use CORS policy
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
