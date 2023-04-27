using JokesApi.Models;

namespace JokesApi.Repositories.Language
{
    public interface ILanguagesRepository : ICRUDRepository<LanguageModel, int>
    {
        public Task<LanguageModel?> GetByNameOrIdAsync(string id);
        public Task<LanguageModel?> GetByNameAsync(string name);
        public Task<bool> ExistsByNameAsync(string name);
    }
}
