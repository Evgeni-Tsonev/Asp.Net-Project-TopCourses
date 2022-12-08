namespace TopCourses.Core.Models.ShoppingCart
{
    using TopCourses.Core.Models.ApplicationFile;

    public class ShoppingCartCourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorFullName { get; set; }

        public ImageFileViewModel Image { get; set; }

        public decimal Price { get; set; }
    }
}
