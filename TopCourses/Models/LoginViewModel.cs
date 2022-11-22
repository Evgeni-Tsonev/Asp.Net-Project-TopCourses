namespace TopCourses.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authentication;

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
