namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Order;
    using TopCourses.Core.Models.ShoppingCart;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly IDbRepository repository;

        public OrderService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> AddOrder(OrderViewModel model, string userId)
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);

            var courses = new List<Course>();

            foreach (var course in model.Courses)
            {
                courses.Add(await this.repository.GetByIdAsync<Course>(course.Id));
            }

            if (string.IsNullOrEmpty(model.TransactionId))
            {
                model.TransactionId = "none";
            }

            var order = new Order()
            {
                Customer = user,
                OrderStatus = model.OrderStatus,
                PaymentStatus = model.PaymentStatus,
                OrderTotal = model.TotalPrice,
                OrderDate = model.OrderDate,
                TransactionId = model.TransactionId,
                Courses = courses,
            };

            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrderViewModel> GetOrderById(int orderId)
        {
            var order = await this.repository
                .All<Order>()
                .Where(o => o.Id == orderId)
                .Include(o => o.Courses)
                .ThenInclude(c => c.Image)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.OrderTotal,
                    PaymentStatus = o.PaymentStatus,
                    OrderStatus = o.OrderStatus,
                    Courses = o.Courses.Select(c => new ShoppingCartCourseViewModel()
                    {
                        Id = c.Id,
                        Image = new ImageFileViewModel()
                        {
                            FileName = c.Image.FileName,
                            FileLength = c.Image.FileLength,
                            Bytes = c.Image.Bytes,
                            ContentType = c.Image.ContentType,
                        },
                        Name = c.Title,
                        Price = c.Price,
                        CreatorFullName = c.Creator.FirstName + " " + c.Creator.LastName,
                    }).ToList(),
                }).FirstOrDefaultAsync();

            return order;
        }

        public Task<IEnumerable<Order>> GetUserOrders(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> OrderProductsByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOrder(OrderViewModel model)
        {
            var order = await this.repository.GetByIdAsync<Order>(model.Id);

            order.PaymentStatus = model.PaymentStatus;
            order.OrderStatus = model.OrderStatus;
            order.OrderDate = model.OrderDate;
            order.TransactionId = model.TransactionId;
            order.OrderTotal = model.TotalPrice;

            this.repository.Update(order);
            await this.repository.SaveChangesAsync();
        }
    }
}
