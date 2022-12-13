namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Language;
    using TopCourses.Infrastructure.Data.Models;

    public class LanguageService : ILanguageService
    {
        private readonly IDbRepository repository;

        public LanguageService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task Add(LanguageViewModel languageModel)
        {
            var language = new Language
            {
                Title = languageModel.Title,
            };

            await this.repository.AddAsync(language);
            await this.repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var language = await this.repository.GetByIdAsync<Language>(id);
            if (language == null)
            {
                throw new ArgumentException(ExceptionMessages.LanguageNotExists);
            }

            language.IsDeleted = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LanguageViewModel>> GetAll()
        {
            return await this.repository.AllReadonly<Language>()
                .Where(l => l.IsDeleted == false)
                .Select(l => new LanguageViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                }).ToListAsync();
        }

        public async Task<LanguageViewModel> GetLanguageForEdit(int id)
        {
            var model = await this.repository
                .GetByIdAsync<Language>(id);

            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.LanguageNotExists);
            }

            return new LanguageViewModel()
            {
                Id = model.Id,
                Title = model.Title,
            };
        }

        public async Task Update(LanguageViewModel model)
        {
            var language = await this.repository
                .GetByIdAsync<Language>(model.Id);

            if (language == null)
            {
                throw new ArgumentException(ExceptionMessages.LanguageNotExists);
            }

            language.Title = model.Title;
            await this.repository.SaveChangesAsync();
        }
    }
}
