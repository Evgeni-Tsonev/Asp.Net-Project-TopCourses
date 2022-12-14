namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Order;
    using TopCourses.Infrastructure.Data.Models;

    public interface IOrderService
    {
        public Task<int> AddOrder(OrderViewModel model, string userId);

        public Task UpdateOrder(OrderViewModel model);

        public Task<OrderViewModel> GetOrderById(int id);

        //public Task<IEnumerable<Order>> OrderProductsByOrderId(int id);
        //public Task<IEnumerable<Order>> GetUserOrders(string name);
    }
}
