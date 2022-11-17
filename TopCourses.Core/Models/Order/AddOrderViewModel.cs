namespace TopCourses.Core.Models.Order
{
    using TopCourses.Core.Models.ShoppingCart;

    public class AddOrderViewModel
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public decimal TotalPrice { get; set; }


        public ICollection<ShoppingCartCourseViewModel> Courses = new List<ShoppingCartCourseViewModel>();
    }
}
