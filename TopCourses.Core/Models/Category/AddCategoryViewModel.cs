namespace TopCourses.Core.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    public class AddCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<AddCategoryViewModel> SubCategories { get; set; } = new HashSet<AddCategoryViewModel>();
    }
}
