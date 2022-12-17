namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TopCourses.Infrastructure.Data.Models;

    public class LanguagesConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasData(this.CreateLanguages());
        }

        private List<Language> CreateLanguages()
        {
            List<Language> languages = new List<Language>()
            {
                new Language()
                {
                    Id = 1,
                    Title = "English",
                },
                new Language()
                {
                    Id = 2,
                    Title = "Bulgarian",
                },
                new Language()
                {
                    Id = 3,
                    Title = "French",
                },
                new Language()
                {
                    Id = 4,
                    Title = "Spanish",
                },
            };

            return languages;
        }
    }
}
