namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Models;

    [TestFixture]
    public class CategoryServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private ICategoryService categoryService;

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
        public async Task CreateCategoryTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);

            await this.categoryService
                .CreateCategory(new CategoryViewModel()
            {
                    Title = "Title",
                    ParentId = null,
            });

            await this.repository.SaveChangesAsync();

            var dbCategory = await this.repository.GetByIdAsync<Category>(1);

            Assert.That(dbCategory, Is.Not.Null);
            Assert.That(dbCategory.Title, Is.EqualTo("Title"));
        }

        [Test]
        public async Task DeleteCategoryTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            await this.repository.AddRangeAsync(new List<Category>()
            {
                new Category() { Id = 1, Title = "First", IsDeleted = false},
                new Category() { Id = 3, Title = "Second", IsDeleted = false},
            });

            await this.repository.SaveChangesAsync();

            await this.categoryService.Delete(1);

            var dbCategory = await this.repository.GetByIdAsync<Category>(1);

            Assert.That(dbCategory.IsDeleted, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteCategoryShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            await this.repository.AddRangeAsync(new List<Category>()
            {
                new Category() { Id = 1, Title = "First", IsDeleted = false},
                new Category() { Id = 2, Title = "Second", IsDeleted = false},
            });

            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.categoryService.Delete(3));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CategoryNotExists));
        }

        [Test]
        public async Task GetAllMainCategoriesTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var categories = new List<Category>()
            {
                new Category() { Id = 1, Title = "First", ParentId = 2 , IsDeleted = false},
                new Category() { Id = 2, Title = "Second", ParentId = null, IsDeleted = false},
                new Category() { Id = 4, Title = "Forth", ParentId = null, IsDeleted = false},
                new Category() { Id = 3, Title = "Third", ParentId = null, IsDeleted = true},
            };

            await this.repository.AddRangeAsync(categories);
            await this.repository.SaveChangesAsync();

            var mainCategories = await this.categoryService.GetAllMainCategories();
            var categoryToAssert = mainCategories.FirstOrDefault(x => x.Id == 2);

            Assert.Multiple(() =>
            {
                Assert.That(categoryToAssert, Is.Not.Null);
                Assert.That(mainCategories.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task GetAllSubCategoriesTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var categories = new List<Category>()
            {
                new Category() { Id = 1, Title = "First", ParentId = 2 , IsDeleted = false},
                new Category() { Id = 2, Title = "Second", ParentId = null, IsDeleted = false},
                new Category() { Id = 4, Title = "Forth", ParentId = 2, IsDeleted = false},
                new Category() { Id = 3, Title = "Third", ParentId = 2, IsDeleted = true},
            };

            await this.repository.AddRangeAsync(categories);
            await this.repository.SaveChangesAsync();

            var mainCategories = await this.categoryService.GetAllSubCategories(2);
            var subcategoryToAssert = mainCategories.FirstOrDefault(x => x.Id == 1);

            Assert.Multiple(() =>
            {
                Assert.That(subcategoryToAssert, Is.Not.Null);
                Assert.That(mainCategories.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task GetCategoryForEditTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var category = new Category() 
            { 
                Id = 1, 
                Title = "First", 
                ParentId = 2, 
                IsDeleted = false 
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();

            var categoryForEdit = await this.categoryService.GetCategoryForEdit(1);

            Assert.That(categoryForEdit, Is.Not.Null);
        }

        [Test]
        public async Task GetCategoryForEditShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var category = new Category()
            {
                Id = 1,
                Title = "First",
                ParentId = 2,
                IsDeleted = false
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.categoryService.GetCategoryForEdit(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CategoryNotExists));
        }

        [Test]
        public async Task UpdateTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var category = new Category()
            {
                Id = 1,
                Title = "First",
                ParentId = 2,
                IsDeleted = false
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();

            await this.categoryService.Update(new EditCategoryViewModel()
            {
                Id = 1,
                Title = "Updated"
            });

            var categoryFromDb = await this.repository.GetByIdAsync<Category>(1);

            Assert.That(categoryFromDb.Title, Is.EqualTo("Updated"));
        }

        [Test]
        public async Task UpdateShouldeThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var category = new Category()
            {
                Id = 1,
                Title = "First",
                ParentId = 2,
                IsDeleted = false
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.categoryService.GetCategoryForEdit(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CategoryNotExists));
        }

        [Test]
        public async Task GetCategoryByIdTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var category = new Category()
            {
                Id = 1,
                Title = "First",
                ParentId = 2,
                IsDeleted = false
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();

            var categoryForEdit = await this.categoryService.GetCategoryById(1);

            Assert.That(categoryForEdit, Is.Not.Null);
        }

        [Test]
        public async Task GetCategoryByIdShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.categoryService = new CategoryService(this.repository);
            var category = new Category()
            {
                Id = 1,
                Title = "First",
                ParentId = 2,
                IsDeleted = false
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.categoryService.GetCategoryById(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CategoryNotExists));
        }



        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
