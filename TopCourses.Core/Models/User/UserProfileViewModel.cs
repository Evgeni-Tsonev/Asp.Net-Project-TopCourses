namespace TopCourses.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }
    }
}
