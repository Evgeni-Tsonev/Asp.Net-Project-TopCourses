namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models;

    public interface ILanguageService
    {
        Task Add(LanguageModel languageModel);

        Task<IEnumerable<LanguageModel>> GetAll();
    }
}
