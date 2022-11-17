namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly IDbRepository repository;

        public OrderService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateOrder(string userId)
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);

        }

        public Task<Order> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetUserOrders(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> OrderProductsByOrderId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
