namespace TopCourses.Infrastructure.Data.Models
{
    using TopCourses.Infrastructure.Data.Identity;

    public class ShoppingCart
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Course> ShoppingCartCourses { get; set; } = new HashSet<Course>();
    }
}
