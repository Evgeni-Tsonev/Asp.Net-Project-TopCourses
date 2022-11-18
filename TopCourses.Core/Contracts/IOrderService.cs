namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Order;
    using TopCourses.Infrastructure.Data.Models;

    public interface IOrderService
    {
        public Task<int> AddOrder(OrderViewModel model, string userId);

        public Task<IEnumerable<Order>> GetUserOrders(string name);

        public Task UpdateOrder(OrderViewModel model);

        public Task<IEnumerable<Order>> OrderProductsByOrderId(int id);

        public Task<OrderViewModel> GetOrderById(int id);
    }
}
