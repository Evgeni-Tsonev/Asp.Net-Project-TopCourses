﻿namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Language;
    using TopCourses.Core.Models.Topic;
    using TopCourses.Infrastructure.Data.Constants;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class EditCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            DataConstants.CourseTitleMaxLength,
            MinimumLength = DataConstants.CourseTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.CourseSubTitleMaxLength,
            MinimumLength = DataConstants.CourseSubTitleMinLength)]
        public string Subtitle { get; set; } = null!;

        [ValidateNever]
        public byte[] Image { get; set; } = Array.Empty<byte>();

        [Required]
        [StringLength(
            DataConstants.CourseGoalsMaxLength,
            MinimumLength = DataConstants.CourseGoalsMinLength)]
        public string Goals { get; set; } = null!;

        [Required]
        [StringLength(
            DataConstants.CourseRequirementsMaxLength,
            MinimumLength = DataConstants.CourseRequirementsMinLength)]
        public string Requirements { get; set; } = null!;

        public IList<TopicViewModel> Curriculum { get; set; } = new List<TopicViewModel>();

        public Level Level { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Subcategory")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required]
        [StringLength(
            DataConstants.CourseDescriptionMaxLength,
            MinimumLength = DataConstants.CourseDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Range(0.00, 2000.00, ErrorMessage = "Price must be a positive number and less than {2} leva")]
        public decimal Price { get; set; }

        public IEnumerable<LanguageViewModel> Languages { get; set; } = new HashSet<LanguageViewModel>();

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
