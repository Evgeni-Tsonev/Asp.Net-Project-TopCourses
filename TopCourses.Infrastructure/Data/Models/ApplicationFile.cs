﻿namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationFile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FileName { get; set; }

        [Required]
        public string SourceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContentType { get; set; }

        [Required]
        public long FileLength { get; set; }
    }
}
