namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Order;
    using TopCourses.Core.Models.ShoppingCart;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    [TestFixture]
    public class OrderServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private IOrderService orderService;

        [SetUp]
        public void Setup()
        {
            var contextOptopns = new DbContextOptionsBuilder<TopCoursesDbContext>()
                .UseInMemoryDatabase("TopCourses")
                .Options;

            this.context = new TopCoursesDbContext(contextOptopns);
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }

        [Test]
        public async Task AddOrderTest()
        {
            this.repository = new DbRepository(this.context);
            this.orderService = new OrderService(this.repository);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);

            await this.repository.SaveChangesAsync();

            var orderId = await this.orderService
                .AddOrder(new OrderViewModel()
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    TotalPrice = 123m,
                    PaymentStatus = "678",
                    OrderStatus = "345",
                    TransactionId = "123",
                }, "1");

            var dbOrder = await this.repository.GetByIdAsync<Order>(1);

            Assert.That(dbOrder, Is.Not.Null);
            Assert.That(dbOrder.Id, Is.EqualTo(orderId));
        }

        [Test]
        public async Task AddOrderTransactionIdNullTest()
        {
            this.repository = new DbRepository(this.context);
            this.orderService = new OrderService(this.repository);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);

            await this.repository.SaveChangesAsync();

            var orderId = await this.orderService
                .AddOrder(new OrderViewModel()
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    TotalPrice = 123m,
                    PaymentStatus = "678",
                    OrderStatus = "345",
                    TransactionId = "",
                }, "1");

            var dbOrder = await this.repository.GetByIdAsync<Order>(1);

            Assert.That(dbOrder, Is.Not.Null);
            Assert.That(dbOrder.Id, Is.EqualTo(orderId));
            Assert.That(dbOrder.TransactionId, Is.EqualTo("none"));
        }

        [Test]
        public async Task GetOrderByIdTest()
        {
            this.repository = new DbRepository(this.context);
            this.orderService = new OrderService(this.repository);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var order = new Order()
            {
                Id = 1,
                CustomerId = "1",
                OrderTotal = 10m,
                OrderDate = DateTime.Now,
                PaymentStatus = "asd",
                OrderStatus = "dfg",
                TransactionId = "fgh",
            };

            await this.repository.AddAsync(order);
            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var dbOrder = await this.orderService.GetOrderById(1);

            Assert.That(dbOrder, Is.Not.Null);
        }

        [Test]
        public async Task UpdateOrderTest()
        {
            this.repository = new DbRepository(this.context);
            this.orderService = new OrderService(this.repository);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var order = new Order()
            {
                Id = 1,
                CustomerId = "1",
                OrderTotal = 10m,
                PaymentStatus = "",
                OrderStatus = "",
                TransactionId = "",
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();

            await this.orderService.UpdateOrder(new OrderViewModel()
            {
                Id = 1,
                TotalPrice = 100m,
                PaymentStatus = "Edited",
                OrderStatus = "Edited",
                TransactionId = "Edited",
            });

            var dbOrder = await this.repository.GetByIdAsync<Order>(1);

            Assert.Multiple(() =>
            {
                Assert.That(dbOrder, Is.Not.Null);
                Assert.That(dbOrder.Id, Is.EqualTo(1));
                Assert.That(dbOrder.OrderTotal, Is.EqualTo(100));
                Assert.That(dbOrder.PaymentStatus, Is.EqualTo("Edited"));
                Assert.That(dbOrder.OrderStatus, Is.EqualTo("Edited"));
                Assert.That(dbOrder.TransactionId, Is.EqualTo("Edited"));
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
