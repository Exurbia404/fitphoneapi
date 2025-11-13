using System;
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
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Column(TypeName = "int")]
        public int UnlockCount { get; set; }

        [Column(TypeName = "int")]
        public int ScreenTimeMinutes { get; set; }

        public void Update(int unlockCount, int screenTimeMinutes)
        {
            UnlockCount = unlockCount;
            ScreenTimeMinutes = screenTimeMinutes;
        }
    }
}
