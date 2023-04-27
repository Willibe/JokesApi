using JokesApi.Models;

namespace JokesApi.Repositories.Joke
{
    public interface IJokesRepository : ICRUDRepository<JokeModel, int>
    {
        public IQueryable<JokeModel> Filter(object id, string propertyName, IQueryable<JokeModel>? queryToAttachTo = null);
    }
}
