namespace TopCourses.Core.Models.Topic
{
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.Video;
    using TopCourses.Core.Models.ApplicationFile;

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

        public ICollection<AddFileViewModel> FilesInfo { get; set; } = new List<AddFileViewModel>();
    }
}
