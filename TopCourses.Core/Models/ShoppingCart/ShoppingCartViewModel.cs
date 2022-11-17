namespace TopCourses.Core.Models.ShoppingCart
{
    using TopCourses.Core.Models.Order;

    public class ShoppingCartViewModel
    {
        public ICollection<ShoppingCartCourseViewModel> Courses { get; set; } = new List<ShoppingCartCourseViewModel>();

        public AddOrderViewModel Order { get; set; }
    }
}
