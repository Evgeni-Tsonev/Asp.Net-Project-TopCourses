namespace TopCourses.Core.Models.Topic
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Video;
    using TopCourses.Infrastructure.Data.Constants;

    public class AddTopicViewModel
    {
        [Required]
        [StringLength(
            DataConstants.TopicTitleMaxLength,
            MinimumLength = DataConstants.TopicTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(
            DataConstants.TopicDescriptionMaxLength,
            MinimumLength = DataConstants.TopicDescriptionMinLength)]
        public string Description { get; set; } = null!;

        public IList<AddVideoViewModel> Videos { get; set; } = new List<AddVideoViewModel>();

        public ICollection<IFormFile> Files { get; set; } = new List<IFormFile>();

        public ICollection<FileViewModel> FilesInfo { get; set; } = new List<FileViewModel>();
    }
}
