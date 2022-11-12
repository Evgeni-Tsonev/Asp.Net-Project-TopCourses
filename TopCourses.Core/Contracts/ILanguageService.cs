namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Language;

    public interface ILanguageService
    {
        Task Add(LanguageViewModel languageModel);

        Task<LanguageViewModel> GetLanguageForEdit(int id);

        Task Update(LanguageViewModel languageModel);

        Task Delete(int id);

        Task<IEnumerable<LanguageViewModel>> GetAll();
    }
}
