namespace TopCourses.Core.Models.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Display(Name = "Full Name")]
        public string UserFullName { get; set; } = null!;

        [Display(Name = "Total Price")]
        public decimal TotalPrice => Courses.Sum(c => c.Price);

        public ICollection<ShoppingCartCourseViewModel> Courses { get; set; } = new List<ShoppingCartCourseViewModel>();
    }
}
