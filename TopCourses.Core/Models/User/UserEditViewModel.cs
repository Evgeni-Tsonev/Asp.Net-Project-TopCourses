namespace TopCourses.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public class UserEditViewModel
    {
        [ValidateNever]
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Image")]
        [ValidateNever]
        public byte[] ProfileImage { get; set; } = Array.Empty<byte>();
    }
}
