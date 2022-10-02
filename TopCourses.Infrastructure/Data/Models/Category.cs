namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public int? ParentID { get; set; }
        public Category? Parent { get; set; }
    }
}