namespace TopCourses.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }
    }
}
