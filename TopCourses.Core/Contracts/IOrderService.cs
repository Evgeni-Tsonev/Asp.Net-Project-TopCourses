namespace TopCourses.Core.Contracts
{
    using TopCourses.Infrastructure.Data.Models;

    public interface IOrderService
    {
        public Task CreateOrder(string userId);

        public Task<IEnumerable<Order>> GetUserOrders(string name);

        public Task<IEnumerable<Order>> OrderProductsByOrderId(int id);

        public Task<Order> GetOrderById(int id);
    }
}
