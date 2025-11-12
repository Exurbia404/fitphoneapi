using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Business.Services;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICreatable<User>, UserService>();
builder.Services.AddScoped<IReadable<User>, UserService>();
builder.Services.AddScoped<IUpdatable<User>, UserService>();
builder.Services.AddScoped<IDeletable<User>, UserService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Scan for profiles and add to automapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add business services
builder.Services.AddScoped<IPhoneUsageService, PhoneUsageService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

var app = builder.Build();
    
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitPhone API v1"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();