namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.User;

    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
    }
}
