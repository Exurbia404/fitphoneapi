using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Controllers.DTOs;
namespace FitPhoneBackend.Business.Interfaces{
    public interface IAuthService
    {
        Task<User> VerifyUserAsync(UserLoginDto dto);    
    }
}