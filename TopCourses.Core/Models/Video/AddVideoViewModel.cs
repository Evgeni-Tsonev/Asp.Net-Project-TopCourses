namespace TopCourses.Core.Models.Video
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class AddVideoViewModel
    {
        [Required]
        [StringLength(
            DataConstants.VideoTitleMaxLength,
            MinimumLength = DataConstants.VideoTitleMinLength)]
        public string Title { get; set; } = null!;

        [Url]
        [Required]
        [Display(Name = "Video Url")]
        public string VideoUrl { get; set; } = null!;
    }
}
