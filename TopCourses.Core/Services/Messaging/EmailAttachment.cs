namespace TopCourses.Services.Messaging
{
    public class EmailAttachment
    {
        public byte[] Content { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string MimeType { get; set; } = null!;
    }
}
