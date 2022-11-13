namespace TopCourses.Core.Models.Review
{
    using System.ComponentModel.DataAnnotations;

    public class AddReviewViewModel
    {
        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [Range(0, 5)]
        public double Rating { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public int CourseId { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}
