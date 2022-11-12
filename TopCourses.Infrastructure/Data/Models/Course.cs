namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Subtitle { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string  Goals = null!;

        [Required]
        [StringLength(1000)]
        public string Requirements = null!;

        public ICollection<Section> Curriculum { get; set; } = new HashSet<Section>();

        public Level Level { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Lenguage { get; set; } = null!;

        [Required]
        [StringLength(1500)]
        public string Description { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string CreatorId { get; set; } = null!;

        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser Creator { get; set; } = null!;

        public ICollection<CourseApplicationUser> Students { get; set; } = new HashSet<CourseApplicationUser>();

        public ICollection<ShoppingCart> ShoppingCart { get; set; } = new HashSet<ShoppingCart>();
    }
}
