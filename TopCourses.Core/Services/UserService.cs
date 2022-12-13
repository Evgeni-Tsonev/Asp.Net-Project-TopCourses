namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
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

        public async Task<UserProfileViewModel> GetUserProfile(string id)
        {
            var user = await this.repository
                .GetByIdAsync<ApplicationUser>(id);

            if (user == null)
            {
                throw new ArgumentException(ExceptionMessages.UserNotExists);
            }

            return new UserProfileViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                ProfileImage = user.ProfileImage,
            };
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await this.repository
                .GetByIdAsync<ApplicationUser>(id);

            if (user == null)
            {
                throw new ArgumentException(ExceptionMessages.UserNotExists);
            }

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImage = user.ProfileImage,
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await this.repository.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Username = u.UserName,
                    ProfileImage = u.ProfileImage,
                })
                .ToListAsync();

            return users;
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await this.repository
                .GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                {
                    user.ProfileImage = model.ProfileImage;
                }

                await this.repository.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
