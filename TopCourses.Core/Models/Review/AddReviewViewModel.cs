namespace TopCourses.Core.Models.Review
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class AddReviewViewModel
    {
        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [MaxLength(5)]
        [MinLength(0)]
        public double Rating { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
