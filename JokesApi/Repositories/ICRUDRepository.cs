namespace JokesApi.Repositories
{
    public interface ICRUDRepository<TEntityModel, TIdentifierType>
    {
        public Task<List<TEntityModel>> GetAllAsync();
        public Task<TEntityModel?> GetByIdAsync(TIdentifierType id);
        public Task<TEntityModel> CreateAsync(TEntityModel model);
        public Task<TEntityModel?> UpdateAsync(TIdentifierType id, TEntityModel model);
        public Task<bool> DeleteAsync(TIdentifierType id);
        public Task<bool> ExistsByIdAsync(TIdentifierType id);

    }
}
