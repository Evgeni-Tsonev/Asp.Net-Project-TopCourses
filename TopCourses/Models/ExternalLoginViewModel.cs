namespace TopCourses.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [ValidateNever]
        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }
    }
}
