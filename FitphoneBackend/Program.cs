using FitPhoneBackend.Business.Services;
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;


Env.Load();

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"); 

if(jwtKey  == null)
{
    Console.WriteLine("JWT KEY was not loaded");
}    

var builder = WebApplication.CreateBuilder(args);

//Scan for profiles and add to automapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<ICreatable<User>, UserService>();
builder.Services.AddScoped<IReadable<User>, UserService>();
builder.Services.AddScoped<IUpdatable<User>, UserService>();
builder.Services.AddScoped<IDeletable<User>, UserService>();

builder.Services.AddScoped<ICreatable<Challenge>, ChallengeService>();
builder.Services.AddScoped<IReadable<Challenge>, ChallengeService>();
builder.Services.AddScoped<IUpdatable<Challenge>, ChallengeService>();
builder.Services.AddScoped<IDeletable<Challenge>, ChallengeService>();

builder.Services.AddScoped<ICreatable<Education>, EducationService>();
builder.Services.AddScoped<IReadable<Education>, EducationService>();
builder.Services.AddScoped<IUpdatable<Education>, EducationService>();
builder.Services.AddScoped<IDeletable<Education>, EducationService>();

builder.Services.AddScoped<ICreatable<Goal>, GoalService>();
builder.Services.AddScoped<IReadable<Goal>, GoalService>();
builder.Services.AddScoped<IUpdatable<Goal>, GoalService>();
builder.Services.AddScoped<IDeletable<Goal>, GoalService>();

builder.Services.AddScoped<ICreatable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<IReadable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<IUpdatable<PhoneUsage>, PhoneUsageService>();
builder.Services.AddScoped<IDeletable<PhoneUsage>, PhoneUsageService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 // JWT Auth 
 builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = jwtIssuer,
             ValidAudience = jwtAudience,
             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(jwtKey)
             )
         };
     });
 builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();
    
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitPhone API v1"));

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();