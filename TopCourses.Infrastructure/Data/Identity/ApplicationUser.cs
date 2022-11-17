namespace TopCourses.Infrastructure.Data.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Models;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public int? ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }

        public ICollection<Course> CoursesCreated { get; set; } = new HashSet<Course>();

        public ICollection<CourseApplicationUser> CoursesEnrolled { get; set; } = new HashSet<CourseApplicationUser>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
