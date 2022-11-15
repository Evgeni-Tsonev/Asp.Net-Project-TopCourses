namespace TopCourses.Core.Models.Topic
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.Video;

    public class AddTopicViewModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        //to do
        //public File Resources { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public IList<AddVideoViewModel> Videos { get; set; } = new List<AddVideoViewModel>();
    }
}
