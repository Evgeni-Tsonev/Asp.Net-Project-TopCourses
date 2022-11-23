namespace TopCourses.Models
{
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Language;

    public class AllCoursesQueryModel
    {
        public const int CoursesPerPage = 9;

        public string? Category { get; set; }

        public string? SubCategory { get; set; }

        public string? Language { get; set; }

        public string? SearchTerm { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public CourseSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<CategoryViewModel> Categories { get; set; } = Enumerable.Empty<CategoryViewModel>();

        public IEnumerable<LanguageViewModel> Languages { get; set; } = Enumerable.Empty<LanguageViewModel>();

        public IEnumerable<CourseListingViewModel> Courses { get; set; } = Enumerable.Empty<CourseListingViewModel>();
    }
}
