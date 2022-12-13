namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using TopCourses.Infrastructure.Data.Constants;
    using TopCourses.Infrastructure.Data.Identity;

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; } = null!;

        public ApplicationUser Customer { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotal { get; set; }

        [Required]
        [StringLength(DataConstants.OrderPaymentStatusMaxLength)]
        public string PaymentStatus { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.OrderStatusMaxLength)]
        public string OrderStatus { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.OrderTransactionIdMaxLength)]
        public string TransactionId { get; set; } = null!;

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
