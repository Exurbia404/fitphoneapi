using FitPhoneBackend.Business.Entities;

namespace FitPhoneBackend.Business.DTOs.UserDto
{

    public class UserCreateDto
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public UsageReason UsageReason { get; set; }
    }
}