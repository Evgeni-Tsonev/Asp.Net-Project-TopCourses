namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.ApplicationFile;

    public interface IImageService
    {
        Task<int> UploadImage(ImageFileViewModel image);
    }
}
