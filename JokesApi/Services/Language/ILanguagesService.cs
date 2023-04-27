using JokesApi.DTOs.Author;
using JokesApi.DTOs.Language;

namespace JokesApi.Services.Language
{
    public interface ILanguagesService : ICRUDServices<LanguageCreate, LanguageUpdate, LanguageView, int>
    {
        public Task<LanguageView?> GetByNameOrIdAsync(string nameOrId);

    }
}
