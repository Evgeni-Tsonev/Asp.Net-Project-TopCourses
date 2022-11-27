namespace TopCourses.Areas.Admin.Models
{
    using TopCourses.Core.Models.Category;

    public class SubCategoriesListModel
    {
        public int MainCategoryId { get; set; }

        public IEnumerable<CategoryViewModel> SubCategories { get; set; }
    }
}
