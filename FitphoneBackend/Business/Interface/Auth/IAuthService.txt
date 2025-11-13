using FitPhoneBackend.Business.Entities;
using FitphoneBackend.Controllers.DTOs;
namespace FitphoneBackend.Business.Interfaces{
    public interface IAuthService
    {
        Task<User> VerifyUserAsync(UserLoginDto dto);    
    }
}