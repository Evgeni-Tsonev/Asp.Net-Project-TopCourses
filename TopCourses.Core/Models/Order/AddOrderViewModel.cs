namespace TopCourses.Core.Models.Order
{
    using TopCourses.Core.Models.ShoppingCart;

    public class AddOrderViewModel
    {
        public decimal TotalPrice { get; set; }

        public ICollection<ShoppingCartCourseViewModel> Courses = new List<ShoppingCartCourseViewModel>();
    }
}
