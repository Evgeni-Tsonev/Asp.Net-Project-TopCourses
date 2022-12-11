namespace TopCourses.Core.Models.Topic
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Video;

    public class AddTopicViewModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public IList<AddVideoViewModel> Videos { get; set; } = new List<AddVideoViewModel>();

        public ICollection<IFormFile> Files { get; set; } = new List<IFormFile>();

        public ICollection<FileViewModel> FilesInfo { get; set; } = new List<FileViewModel>();
    }
}
