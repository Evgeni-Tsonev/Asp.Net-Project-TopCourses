namespace TopCourses.Infrastructure.Data.Identity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using TopCourses.Infrastructure.Data.Constants;
    using TopCourses.Infrastructure.Data.Models;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(DataConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public byte[] ProfileImage { get; set; }

        public bool IsDeleted { get; set; }

        public int? ShoppingCartId { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }

        public ICollection<Course> CoursesCreated { get; set; } = new HashSet<Course>();

        public ICollection<CourseApplicationUser> CoursesEnrolled { get; set; } = new HashSet<CourseApplicationUser>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
