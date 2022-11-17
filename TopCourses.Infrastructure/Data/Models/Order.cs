namespace TopCourses.Infrastructure.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TopCourses.Infrastructure.Data.Identity;

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotal { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string TransactionId { get; set; } = null!;

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
