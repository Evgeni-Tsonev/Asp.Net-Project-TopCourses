namespace TopCourses.Core.Models.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartCourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorFullName { get; set; }

        public byte[] Image { get; set; }

        public decimal Price { get; set; }
    }
}
