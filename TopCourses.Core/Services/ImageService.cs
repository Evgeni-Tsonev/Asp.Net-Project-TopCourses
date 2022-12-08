namespace TopCourses.Core.Services
{
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Infrastructure.Data.Models;

    public class ImageService : IImageService
    {
        private readonly IDbRepository repository;

        public ImageService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> UploadImage(ImageFileViewModel model)
        {
            var image = new ImageFile()
            {
                FileName = model.FileName,
                FileLength = model.FileLength,
                ContentType = model.ContentType,
                Bytes = model.Bytes,
            };

            await this.repository.AddAsync(image);
            await this.repository.SaveChangesAsync();

            return image.Id;
        }
    }
}
