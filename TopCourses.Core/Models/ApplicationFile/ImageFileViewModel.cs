namespace TopCourses.Core.Models.ApplicationFile
{
    using System.ComponentModel.DataAnnotations;

    public class ImageFileViewModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public long FileLength { get; set; }

        public byte[] Bytes { get; set; }
    }
}
