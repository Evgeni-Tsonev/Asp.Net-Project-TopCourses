namespace TopCourses.Core.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.ShoppingCart;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDbRepository repository;

        public ShoppingCartService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddCourseInShoppingCart(int courseId, string userId)
        {
            var course = await this.repository.GetByIdAsync<Course>(courseId);
            var user = await this.repository
                .All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(sc => sc.ShoppingCart)
                .ThenInclude(c => c.ShoppingCartCourses)
                .Include(c => c.CoursesEnrolled)
                .ThenInclude(uc => uc.Course)
                .FirstOrDefaultAsync();

            if (course == null || user == null)
            {
                throw new Exception();
            }

            if (user.CoursesEnrolled.Any(c => c.Course.Id == courseId))
            {
                throw new Exception("Already have that course");
            }

            if (user.ShoppingCartId == null)
            {
                var shoppingCart = new ShoppingCart()
                {
                    UserId = user.Id,
                };

                await this.repository.AddAsync(shoppingCart);
                user.ShoppingCartId = shoppingCart.Id;
            }

            if (user.ShoppingCart.ShoppingCartCourses.Any(c => c.Id == courseId))
            {
                return;
            }

            user.ShoppingCart.ShoppingCartCourses.Add(course);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteAllCoursesFromShoppingCart(string userId)
        {
            var user = await this.repository
                 .All<ApplicationUser>()
                 .Where(u => u.Id == userId)
                 .Include(sc => sc.ShoppingCart)
                 .ThenInclude(c => c.ShoppingCartCourses)
                 .FirstOrDefaultAsync();

            if (user == null)
            {
                return;
            }

            user.ShoppingCart.ShoppingCartCourses.Clear();
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteCourseFromShoppingCart(int courseId, string userId)
        {
            var user = await this.repository
                .All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(sc => sc.ShoppingCart)
                .ThenInclude(c => c.ShoppingCartCourses)
                .FirstOrDefaultAsync();

            var course = user.ShoppingCart.ShoppingCartCourses.FirstOrDefault(c => c.Id == courseId);

            if (user == null || course == null)
            {
                return;
            }

            user.ShoppingCart.ShoppingCartCourses.Remove(course);
            await this.repository.SaveChangesAsync();
        }

        public async Task<ShoppingCartViewModel> GetShoppingCart(string userId)
        {
            var user = await this.repository
                .All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(sc => sc.ShoppingCart)
                .ThenInclude(c => c.ShoppingCartCourses)
                .ThenInclude(c => c.Creator)
                .Include(sc => sc.ShoppingCart)
                .ThenInclude(c => c.ShoppingCartCourses)
                .ThenInclude(c => c.Image)
                .FirstOrDefaultAsync();

            if (user == null || user.ShoppingCartId == null)
            {
                return null;
            }

            var courses = user.ShoppingCart?
                .ShoppingCartCourses
                .Select(c => new ShoppingCartCourseViewModel()
                {
                    Id = c.Id,
                    Name = c.Title,
                    CreatorFullName = c.Creator.FirstName + " " + c.Creator.LastName,
                    Image = c.Image,
                    Price = c.Price,
                }).ToList();

            var cart = new ShoppingCartViewModel()
            {
                UserFullName = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                Email = user.Email,
                Courses = courses,
            };

            return cart;
        }
    }
}
