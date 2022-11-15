namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public int? ParentId { get; set; }
        public virtual Category? Parent { get; set; }

        public virtual ICollection<Category> SubCategory { get; set; } = new HashSet<Category>();

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public ICollection<Course> CoursesSubCategories { get; set; } = new HashSet<Course>();
    }
}