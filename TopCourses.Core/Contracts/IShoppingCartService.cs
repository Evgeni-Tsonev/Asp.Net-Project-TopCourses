namespace TopCourses.Core.Contracts
{
    using TopCourses.Infrastructure.Data.Models;

    public interface IShoppingCartService
    {
        Task AddCourseInShoppingCart(int courseId, string userId);

        Task<IEnumerable<Course>> GetAllShoppingCartCoursess(string userId);

        Task DeleteCourseFromShoppingCart(int id, string userId);

        Task DeleteAllCoursesFromShoppingCart(string userId);
    }
}
