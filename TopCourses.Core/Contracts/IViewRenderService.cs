namespace TopCourses.Core.Contracts
{
    public interface IViewRenderService
    {
        Task<string> RenderToString(string viewName, object model);
    }
}
