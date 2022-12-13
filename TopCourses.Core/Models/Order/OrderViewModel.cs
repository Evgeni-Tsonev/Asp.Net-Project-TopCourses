namespace TopCourses.Core.Models.Order
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.ShoppingCart;

    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; } = null!;

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; } = null!;

        [Display(Name = "Transaction")]
        public string TransactionId { get; set; } = null!;

        public ICollection<ShoppingCartCourseViewModel> Courses = new List<ShoppingCartCourseViewModel>();
    }
}
