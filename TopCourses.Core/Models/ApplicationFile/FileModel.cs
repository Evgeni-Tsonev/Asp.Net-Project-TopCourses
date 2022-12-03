namespace TopCourses.Core.Models.ApplicationFile
{
    using System.ComponentModel.DataAnnotations;

    public class FileModel
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string SourceId { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public long FileLength { get; set; }
    }
}
