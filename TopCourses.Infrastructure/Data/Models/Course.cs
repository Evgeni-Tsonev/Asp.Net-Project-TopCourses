namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TopCourses.Infrastructure.Data.Constants;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.CourseTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.CourseSubTitleMaxLength)]
        public string Subtitle { get; set; } = null!;

        public byte[] Image { get; set; }

        [Required]
        [StringLength(DataConstants.CourseGoalsMaxLength)]
        public string Goals { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.CourseRequirementsMaxLength)]
        public string Requirements { get; set; } = null!;

        public ICollection<Topic> Curriculum { get; set; } = new HashSet<Topic>();

        public Level Level { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public int SubCategoryId { get; set; }

        public Category SubCategory { get; set; } = null!;

        public int LanguageId { get; set; }

        public Language Language { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.CourseDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string CreatorId { get; set; } = null!;

        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser Creator { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdate { get; set; }

        [NotMapped]
        public double Rating
            => this.Reviews.Count > 0 ? this.Reviews.Average(x => x.Rating) : 0;

        public ICollection<CourseApplicationUser> Students { get; set; } = new HashSet<CourseApplicationUser>();

        public ICollection<ShoppingCart> ShoppingCart { get; set; } = new HashSet<ShoppingCart>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
