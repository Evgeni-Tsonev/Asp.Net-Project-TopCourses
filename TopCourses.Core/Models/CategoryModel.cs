namespace TopCourses.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Models;

    public class CategoryModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Category> SubCategory { get; set; } = new HashSet<Category>();
    }
}
