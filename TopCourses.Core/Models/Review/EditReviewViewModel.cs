namespace TopCourses.Core.Models.Review
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class EditReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.ReviewCommentMaxLength)]
        public string Comment { get; set; } = null!;

        [Range(
            DataConstants.ReviewRatingMinLength,
            DataConstants.ReviewRatingMaxLength)]
        public int Rating { get; set; }

        public DateTime LastUpdate { get; set; }

        public string UserId { get; set; }
    }
}
