namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.User;
    using TopCourses.Infrastructure.Data.Identity;

    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);
    }
}
