using FitPhoneBackend.Business.Entities;

namespace FitPhoneBackend.API.Utils
{
    public static class UserUtils
    {
        public static User SanitizeUser(User user)
        {
            return new User(
                user.Id,
                user.Username,
                user.Email,
                user.UsageReason
            );
        }
    }
}