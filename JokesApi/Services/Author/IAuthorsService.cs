using JokesApi.DTOs.Author;
using JokesApi.DTOs.Joke;

namespace JokesApi.Services.Author
{
    public interface IAuthorsService : ICRUDServices<AuthorCreate, AuthorUpdate, AuthorView, int>
    {
        public Task<AuthorView?> GetByNameOrIdAsync(string nameOrId);
    }
}
