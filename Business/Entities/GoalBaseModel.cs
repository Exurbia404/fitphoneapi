using System.ComponentModel.DataAnnotations;

namespace FitPhoneBackend.Business.Entities
{
    public abstract class GoalBaseModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentProgress { get; set; }
        public int TargetGoal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}