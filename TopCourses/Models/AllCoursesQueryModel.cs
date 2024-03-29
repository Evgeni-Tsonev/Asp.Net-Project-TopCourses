﻿namespace TopCourses.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Language;

    public class AllCoursesQueryModel
    {
        public const int CoursesPerPage = 6;

        public string? Category { get; set; }

        public string? SubCategory { get; set; }

        public string? Language { get; set; }

        [Display(Name = "Search Term")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Min price")]
        public decimal MinPrice { get; set; } = 0;

        [Display(Name = "Max price")]
        public decimal MaxPrice { get; set; } = 2000;

        public CourseSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCoursesCount { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = Enumerable.Empty<CategoryViewModel>();

        public IEnumerable<LanguageViewModel> Languages { get; set; } = Enumerable.Empty<LanguageViewModel>();

        public IEnumerable<CourseListingViewModel> Courses { get; set; } = Enumerable.Empty<CourseListingViewModel>();
    }
}
