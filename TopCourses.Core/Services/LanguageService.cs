namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Core.Data.Common;

    public class LanguageService : ILanguageService
    {
        private readonly IDbRepository repository;

        public LanguageService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task Add(LanguageModel languageModel)
        {
            var language = new Language
            {
                Title = languageModel.Title,
            };

            await this.repository.AddAsync(language);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LanguageModel>> GetAll()
        {
            return await this.repository.AllReadonly<Language>()
                .Where(l => l.IsDeleted == false)
                .Select(l => new LanguageModel
                {
                    Id = l.Id,
                    Title = l.Title
                }).ToListAsync();
        }
    }
}
