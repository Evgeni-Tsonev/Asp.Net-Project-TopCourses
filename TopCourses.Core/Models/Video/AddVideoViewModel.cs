namespace TopCourses.Core.Models.Video
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.Topic;

    public class AddVideoViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Url]
        [Required]
        [Display(Name = "Video Url")]
        public string VideoUrl { get; set; } = null!;
    }
}
