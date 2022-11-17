namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.ShoppingCart;

    public interface IShoppingCartService
    {
        Task AddCourseInShoppingCart(int courseId, string userId);

        Task<ShoppingCartViewModel> GetShoppingCart(string userId);

        Task DeleteCourseFromShoppingCart(int id, string userId);

        Task DeleteAllCoursesFromShoppingCart(string userId);
    }
}
