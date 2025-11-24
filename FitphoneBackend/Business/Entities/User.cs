using System;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public PhoneUsage? PhoneUsage { get; set; } = null; //1-1 relationship, phone usage belongs to only one user

        // public ICollection<Garden> Gardens { get; set; } = new List<Garden>();

        public User(Guid id, string username, string email, UsageReason usageReason, PhoneUsage phoneUsage)
        {
            Id = id;
            Username = username;
            Email = email;
            UsageReason = usageReason;
            PhoneUsage = phoneUsage;
        }
        public User(Guid id, string username, string email, UsageReason usageReason)
        {
            Id = id;
            Username = username;
            Email = email;
            UsageReason = usageReason;
            
        }
    }
}