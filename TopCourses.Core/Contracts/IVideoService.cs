namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Video;

    public interface IVideoService
    {
        Task<VideoViewModel> GetVideoById(int id);

        Task<ICollection<AddVideoViewModel>> ReplaceVideoUrls(IList<AddVideoViewModel> videos);
    }
}
