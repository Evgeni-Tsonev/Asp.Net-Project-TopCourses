namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Goal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = null!;

        public int CoirseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
