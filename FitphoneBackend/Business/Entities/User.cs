using System;
using System.ComponentModel.DataAnnotations;

namespace FitPhoneBackend.Business.Entities
{
    public enum UsageReason
    {
        Social,
        Gaming,
        Productivity,
        Entertainment,
        Other
    }

    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required]
        public UsageReason UsageReason { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public PhoneUsage PhoneUsage { get; set; } //1-1 relationship, phone usage belongs to only one user

        // public ICollection<Garden> Gardens { get; set; } = new List<Garden>();
    }
}