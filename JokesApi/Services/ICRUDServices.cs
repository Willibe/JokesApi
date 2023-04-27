using JokesApi.DTOs.Category;

namespace JokesApi.Services
{
    public interface ICRUDServices<TEntityCreate, TEntityUpdate, TEntityView, TIdentifierType>
    {
        public Task<List<TEntityView>> GetAllAsync();
        public Task<TEntityView> CreateAsync(TEntityCreate model);
        public Task<TEntityView?> GetByIdAsync(TIdentifierType id);
        public Task<TEntityView?> UpdateAsync(TIdentifierType id, TEntityUpdate model);
        public Task<bool> DeleteAsync(TIdentifierType id);
    }
}
