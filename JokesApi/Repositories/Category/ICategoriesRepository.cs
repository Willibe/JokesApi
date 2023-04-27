using JokesApi.Models;

namespace JokesApi.Repositories.Category
{
    public interface ICategoriesRepository : ICRUDRepository<CategoryModel, int>
    {
        public Task<CategoryModel?> GetByNameOrIdAsync(string id);
        public Task<CategoryModel?> GetByNameAsync(string name);
        public Task<bool> ExistsByNameAsync(string name);
    }
}
