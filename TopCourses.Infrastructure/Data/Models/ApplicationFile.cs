namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationFile
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string FileName { get; set; } = null!;

        public byte[] Content { get; set; }

        public int SourceId { get; set; }

        public string ContentType { get; set; }
    }
}
