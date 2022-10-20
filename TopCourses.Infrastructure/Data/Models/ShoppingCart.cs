namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Identity;

    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; }

        public ICollection<Course> ShoppingCartCourses { get; set; } = new HashSet<Course>();
    }
}
