namespace TopCourses.Core.Models.User
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] ProfileImage { get; set; }
    }
}
