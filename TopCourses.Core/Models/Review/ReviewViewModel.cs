namespace TopCourses.Core.Models.Review
{
    public class ReviewViewModel
    {
        public string Comment { get; set; } = null!;

        public double Rating { get; set; }

        public string UserFullName { get; set; } = null!;

        public DateTime DateOfPublication { get; set; }
    }
}
