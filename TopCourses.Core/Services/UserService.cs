namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.User;
    using TopCourses.Infrastructure.Data.Identity;

    public class UserService : IUserService
    {
        private readonly IDbRepository repository;

        public UserService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await this.repository.GetByIdAsync<ApplicationUser>(id);
        }

        public Task<UserEditViewModel> GetUserForEdit(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await this.repository.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Username = u.UserName
                })
                .ToListAsync();

            return users;
        }

        public Task<bool> UpdateUser(UserEditViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
