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

        //[Test]
        //public async Task AddOrderTest()
        //{
        //    this.repository = new DbRepository(this.context);
        //    this.orderService = new OrderService(this.repository);

        //    var orderId = await this.orderService
        //        .AddOrder(new OrderViewModel()
        //        {
        //            OrderDate = DateTime.Now,
        //            TotalPrice = 123m,
        //            PaymentStatus = "678",
        //            OrderStatus = "345",
        //            TransactionId = "123",
        //            Courses = new List<ShoppingCartCourseViewModel>() {
        //                new ShoppingCartCourseViewModel()
        //            {
        //                Id = 1,
        //                CreatorFullName = "",
        //                Name = "",
        //                Image = new byte[0],
        //                Price = 12,
        //            }},
        //        }, "5");

        //    await this.repository.SaveChangesAsync();

        //    var dbCategory = await this.repository.GetByIdAsync<Order>(1);

        //    Assert.That(dbCategory, Is.Not.Null);
        //    //Assert.That(dbCategory.Id, Is.EqualTo(orderId));
        //}

        [Test]
        public async Task GetOrderByIdTest()
        {
            this.repository = new DbRepository(this.context);
            this.orderService = new OrderService(this.repository);
            var order = new Order()
            {
                Id = 1,
                CustomerId = "2",
                OrderTotal = 10m,
                OrderDate = DateTime.Now,
                PaymentStatus = "asd",
                OrderStatus = "dfg",
                TransactionId = "fgh",
            };

            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();

            var dbOrder = await this.orderService.GetOrderById(1);

            Assert.That(dbOrder, Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
