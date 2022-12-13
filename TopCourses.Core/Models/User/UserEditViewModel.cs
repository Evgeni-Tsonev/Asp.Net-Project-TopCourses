namespace TopCourses.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using TopCourses.Infrastructure.Data.Constants;

    public class UserEditViewModel
    {
        [ValidateNever]
        public string Id { get; set; }

        [Required]
        [StringLength(
            DataConstants.UserFirstNameMaxLength,
            MinimumLength = DataConstants.UserFirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
            DataConstants.UserLastNameMaxLength,
            MinimumLength = DataConstants.UserLastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Image")]
        [ValidateNever]
        public byte[] ProfileImage { get; set; } = Array.Empty<byte>();
    }
}
