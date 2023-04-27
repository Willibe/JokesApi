using JokesApi.DTOs.Author;
using JokesApi.DTOs.Category;
using JokesApi.DTOs.Joke;

namespace JokesApi.Services.Category
{
    public interface ICategoriesService : ICRUDServices<CategoryCreate, CategoryUpdate, CategoryView, int>
    {
        public Task<CategoryView?> GetByNameOrIdAsync(string nameOrId);

    }
}
