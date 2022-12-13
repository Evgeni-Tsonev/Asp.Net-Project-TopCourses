namespace TopCourses.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using TopCourses.Infrastructure.Data.Constants;

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(
            DataConstants.UserUsernameMaxLength,
            MinimumLength = DataConstants.UserUsernameMinLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(
            DataConstants.UserFirstNameMaxLength,
            MinimumLength = DataConstants.UserFirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(
            DataConstants.UserLastNameMaxLength,
            MinimumLength = DataConstants.UserLastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [ValidateNever]
        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }
    }
}
