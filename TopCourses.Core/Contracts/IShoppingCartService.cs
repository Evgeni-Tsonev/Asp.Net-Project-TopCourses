namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.ShoppingCart;
    using TopCourses.Infrastructure.Data.Models;

    public interface IShoppingCartService
    {
        Task AddCourseInShoppingCart(int courseId, string userId);

        Task<IEnumerable<ShoppingCartCourseViewModel>> GetAllShoppingCartCoursess(string userId);

        Task DeleteCourseFromShoppingCart(int id, string userId);

        Task DeleteAllCoursesFromShoppingCart(string userId);
    }
}
