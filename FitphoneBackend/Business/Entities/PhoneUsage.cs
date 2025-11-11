using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPhoneBackend.Business.Entities
{
    public class PhoneUsage
    {
        public PhoneUsage(Guid id, Guid userId, int unlockCount, int screenTimeMinutes)
        {
            Id = id;
            UserId = userId;
            UnlockCount = unlockCount;
            ScreenTimeMinutes = screenTimeMinutes;
        }

        public PhoneUsage() { }

        [Key]
        public Guid Id { get; private set; }

        [Required]
        public Guid UserId { get; private set; }
    
        public int UnlockCount { get; private set; } //pick ups
        public int ScreenTimeMinutes { get; private set; } 

        public void Update(int unlockCount, int screenTimeMinutes)
        {
            UnlockCount = unlockCount;
            ScreenTimeMinutes = screenTimeMinutes;
        }
    }
}
