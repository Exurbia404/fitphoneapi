using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPhoneBackend.Business.Entities
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(254)")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "longtext")]
        public string VideoURL { get; set; } = null!;

        [Column(TypeName = "longtext")]
        public string ArticleURL { get; set; } = null!;

        [Column(TypeName = "varchar(100)")]
        public string Type { get; set; } = null!; // video or article
    }
}
