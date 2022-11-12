namespace TopCourses.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class UserListViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
