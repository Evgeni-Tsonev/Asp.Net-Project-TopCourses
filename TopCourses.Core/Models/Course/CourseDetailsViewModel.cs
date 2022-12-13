namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using TopCourses.Core.Models.Review;
    using TopCourses.Core.Models.Topic;
    using TopCourses.Core.Models.User;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Subtitle { get; set; } = null!;

        public byte[] Image { get; set; }

        public string Goals = null!;

        public string Requirements = null!;

        public ICollection<TopicViewModel> Curriculum { get; set; } = new HashSet<TopicViewModel>();

        public ICollection<ReviewViewModel> Reviews { get; set; } = new HashSet<ReviewViewModel>();

        public double Rating { get; set; }

        public Level Level { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Subcategory")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdate { get; set; }

        public UserViewModel Creator { get; set; } = null!;
    }
}
