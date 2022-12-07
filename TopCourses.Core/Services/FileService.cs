namespace TopCourses.Core.Services
{
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Infrastructure.Data.Models;

    public class FileService : IFileService
    {
        private readonly IDbRepository data;

        public FileService(IDbRepository data)
        {
            this.data = data;
        }

        public async Task SaveFile(ApplicationFile file)
        {
            await this.data.AddAsync(file);
            await this.data.SaveChangesAsync();
        }
    }
}