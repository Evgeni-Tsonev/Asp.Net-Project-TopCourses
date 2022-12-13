namespace TopCourses.Core.Models.Review
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class AddReviewViewModel
    {
        [Required]
        [StringLength(DataConstants.ReviewCommentMaxLength)]
        public string Comment { get; set; } = null!;

        [Range(
            DataConstants.ReviewRatingMinLength,
            DataConstants.ReviewRatingMaxLength)]
        public int Rating { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public int CourseId { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}
