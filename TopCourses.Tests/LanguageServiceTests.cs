namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Language;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Models;

    public class LanguageServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private ILanguageService languageService;

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
        public async Task AddTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);

            await this.languageService
                .Add(new LanguageViewModel()
                {
                    Title = "Title",
                });

            await this.repository.SaveChangesAsync();

            var dbLanguage = await this.repository.GetByIdAsync<Language>(1);

            Assert.That(dbLanguage, Is.Not.Null);
            Assert.That(dbLanguage.Title, Is.EqualTo("Title"));
        }

        [Test]
        public async Task DeleteLanguageTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            await this.repository.AddRangeAsync(new List<Language>()
            {
                new Language() { Id = 1, Title = "First", IsDeleted = false},
                new Language() { Id = 3, Title = "Second", IsDeleted = false},
            });

            await this.repository.SaveChangesAsync();

            await this.languageService.Delete(1);

            var dbLanguage = await this.repository.GetByIdAsync<Language>(1);

            Assert.That(dbLanguage.IsDeleted, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteLanguageShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            await this.repository.AddRangeAsync(new List<Language>()
            {
                new Language() { Id = 1, Title = "First", IsDeleted = false},
                new Language() { Id = 2, Title = "Second", IsDeleted = false},
            });

            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.languageService.Delete(3));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.LanguageNotExists));
        }

        [Test]
        public async Task GetAllTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            var languages = new List<Language>()
            {
                new Language() { Id = 1, Title = "First", IsDeleted = false},
                new Language() { Id = 2, Title = "Second", IsDeleted = false},
                new Language() { Id = 4, Title = "Forth", IsDeleted = false},
                new Language() { Id = 3, Title = "Third", IsDeleted = true},
            };

            await this.repository.AddRangeAsync(languages);
            await this.repository.SaveChangesAsync();

            var dbLanguages = await this.languageService.GetAll();
            var languageToAssert = dbLanguages.FirstOrDefault(x => x.Id == 2);

            Assert.Multiple(() =>
            {
                Assert.That(languageToAssert, Is.Not.Null);
                Assert.That(dbLanguages.Count, Is.EqualTo(3));
            });
        }

        [Test]
        public async Task GetLanguageForEditTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            var language = new Language()
            {
                Id = 1,
                Title = "First",
                IsDeleted = false
            };

            await this.repository.AddAsync(language);
            await this.repository.SaveChangesAsync();

            var languageForEdit = await this.languageService.GetLanguageForEdit(1);

            Assert.That(languageForEdit, Is.Not.Null);
        }

        [Test]
        public async Task GetLanguageForEditShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            var language = new Language()
            {
                Id = 1,
                Title = "First",
                IsDeleted = false
            };

            await this.repository.AddAsync(language);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.languageService.GetLanguageForEdit(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.LanguageNotExists));
        }

        [Test]
        public async Task UpdateTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            var language = new Language()
            {
                Id = 1,
                Title = "First",
                IsDeleted = false
            };

            await this.repository.AddAsync(language);
            await this.repository.SaveChangesAsync();

            await this.languageService.Update(new LanguageViewModel()
            {
                Id = 1,
                Title = "Updated"
            });

            var categoryFromDb = await this.repository.GetByIdAsync<Language>(1);

            Assert.That(categoryFromDb.Title, Is.EqualTo("Updated"));
        }

        [Test]
        public async Task UpdateShouldeThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.languageService = new LanguageService(this.repository);
            var language = new Language()
            {
                Id = 1,
                Title = "First",
                IsDeleted = false
            };

            await this.repository.AddAsync(language);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.languageService.Update(new LanguageViewModel()
                {
                    Id = 2,
                    Title = "first",
                }));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.LanguageNotExists));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
