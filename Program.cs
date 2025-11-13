using FitPhoneBackend.Business.Services;
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

//Scan for profiles and add to automapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICreatable<User>, UserService>();
builder.Services.AddScoped<IReadable<User>, UserService>();
builder.Services.AddScoped<IUpdatable<User>, UserService>();
builder.Services.AddScoped<IDeletable<User>, UserService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ICreatable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<IReadable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<IUpdatable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<IDeletable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<PhoneUsageService>();

builder.Services.AddScoped<ICreatable<Education>, EducationService>();
builder.Services.AddScoped<IReadable<Education>, EducationService>();
builder.Services.AddScoped<IUpdatable<Education>, EducationService>();
builder.Services.AddScoped<IDeletable<Education>, EducationService>();
builder.Services.AddScoped<EducationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// // JWT Auth 
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = jwtIssuer,
//             ValidAudience = jwtAudience,
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(jwtKey)
//             )
//         };
//     });
// builder.Services.AddAuthorization();


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