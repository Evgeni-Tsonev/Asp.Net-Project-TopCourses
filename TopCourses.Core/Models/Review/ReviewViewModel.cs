namespace TopCourses.Core.Models.Review
{
    using TopCourses.Core.Models.User;

    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string Comment { get; set; } = null!;

        public double Rating { get; set; }

        public string UserFullName { get; set; } = null!;

        public byte[] UserProfileImage { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}
