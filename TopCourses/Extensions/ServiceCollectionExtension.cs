namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.MongoInterfaceses;
    using TopCourses.Services.Messaging;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDbRepository, DbRepository>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IBucket, BucketContex>();
            services.AddScoped<IImageService, ImageService>();

            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(config["SendGrid:ApiKey"]));

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<TopCoursesDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            var mongoConnectionString = config.GetSection("MongoDb:ConnectionString").Value;
            var mongoDatabaseName = config.GetSection("MongoDb:Database").Value;
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = mongoConnectionString;
                options.Database = mongoDatabaseName;
            });

            return services;
        }
    }
}
