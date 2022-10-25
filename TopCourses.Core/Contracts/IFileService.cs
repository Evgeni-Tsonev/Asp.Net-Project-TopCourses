namespace TopCourses.Core.Contracts
{
    using TopCourses.Infrastructure.Data.Models;

    public interface IFileService
    {
        Task SaveFile(ApplicationFile file);
    }
}
