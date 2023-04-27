using JokesApi.DTOs.Author;
using JokesApi.DTOs.Joke;
using JokesApi.DTOs.Language;

namespace JokesApi.Services.Joke
{
    public interface IJokesService : ICRUDServices<JokeCreate, JokeUpdate, JokeView, int>
    {
        public Task<JokeView> UpdatePartially(int id, JokeUpdatePartially jokeUpdatePartially);
        public Task<List<JokeView>> FilterByLanguageAsync(string langIdentifier);
        public Task<List<JokeView>> FilterByLanguageAndCategoryAsync(string langIdentifier, string catIdentifier);
    }
}
