using JokesApi.Models;

namespace JokesApi.Repositories.Author
{
    public interface IAuthorsRepository : ICRUDRepository<AuthorModel, int>
    {
        public Task<AuthorModel?> GetByNameOrIdAsync(string id);
        public Task<AuthorModel?> GetByNameAsync(string name);
        public Task<bool> ExistsByNameAsync(string name);

    }
}
