namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class ApplicationFile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.FileNameMaxLength)]
        public string FileName { get; set; }

        [Required]
        public string SourceId { get; set; }

        [Required]
        [MaxLength(DataConstants.FileContentTypeMaxLength)]
        public string ContentType { get; set; }

        [Required]
        public long FileLength { get; set; }
    }
}
