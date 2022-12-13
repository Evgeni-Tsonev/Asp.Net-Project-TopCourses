namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;
    using TopCourses.Infrastructure.Data.Identity;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.ReviewRatingMaxLength)]
        public string Comment { get; set; } = null!;

        [MaxLength(DataConstants.ReviewRatingMaxLength)]
        public int Rating { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateOfPublication { get; set; }

        public DateTime? LastUpdate { get; set; }

        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;
    }
}
