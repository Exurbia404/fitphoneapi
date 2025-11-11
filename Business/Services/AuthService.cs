using FitphoneBackend.Business.Interfaces;
using FitPhoneBackend.Business.Entities;
using FitphoneBackend.Controllers.DTOs;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FitphoneBackend.Business.Exceptions;
namespace FitphoneBackend.Business.Services{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> VerifyUserAsync(UserLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new MissingFieldException();

            User? userToFetch = await _context.Users
                                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (userToFetch is null)
                throw new InvalidUserException();

            return userToFetch;
            
        }  
    }
}