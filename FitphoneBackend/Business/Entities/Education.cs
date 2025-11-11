using System.ComponentModel.DataAnnotations;

namespace FitPhoneBackend.Business.Entities
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public string ArticleURL { get; set; }
        public string Type { get; set; } //defines if it is a video or an article
    }
}