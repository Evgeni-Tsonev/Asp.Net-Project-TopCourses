namespace TopCourses.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required]
        [Compare(nameof(PasswordRepeat))]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Repeat")]
        public string PasswordRepeat { get; set; } = null!;

        [Required]
        [StringLength(
            DataConstants.UserUsernameMaxLength,
            MinimumLength = DataConstants.UserUsernameMinLength)]
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
