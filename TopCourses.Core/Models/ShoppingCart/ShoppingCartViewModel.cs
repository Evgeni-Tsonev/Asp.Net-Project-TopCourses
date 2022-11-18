namespace TopCourses.Core.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string UserFullName { get; set; } = null!;

        public decimal TotalPrice => Courses.Sum(c => c.Price);

        public ICollection<ShoppingCartCourseViewModel> Courses { get; set; } = new List<ShoppingCartCourseViewModel>();
    }
}
