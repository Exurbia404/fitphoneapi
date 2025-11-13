using System.ComponentModel.DataAnnotations.Schema;

namespace FitPhoneBackend.Business.Entities
{
    [Table("Challenges")]
    public class Challenge : GoalBaseModel
    {
        public string? MissionExplanation { get; set; }
    }
}