using JokesApi.DataContext;
using JokesApi.Helpers;
using JokesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesApi.Repositories.Joke
{
    public class JokesRepository : IJokesRepository
    {
        private readonly JokesDataContext _context;
        public JokesRepository(JokesDataContext context)
        {
            _context = context;
        }

        public async Task<JokeModel> CreateAsync(JokeModel model)
        {
            model.DateAdded = DateTime.Now;
            model.Deleted = false;
            await _context.Jokes.AddAsync(model);
            await _context.SaveChangesAsync();
            await _context.Entry(model).ReloadAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await GetByIdAsync(id);
            if (model == null)
            {
                return false;
            }
            model.Description = null;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool exists = await _context.Jokes.AnyAsync(model => model.Id == id);
            return exists;
        }

        public IQueryable<JokeModel> Filter(object id, string propertyName, IQueryable<JokeModel>? queryToAttachTo = null)
        {

            var filteredJokes = JokeFilter.Filter(id, propertyName,_context.Jokes, queryToAttachTo);

            return filteredJokes;
        }

        public async Task<List<JokeModel>> GetAllAsync()
        {
            var jokes = await _context.Jokes.ToListAsync();
            return jokes;
        }

        public async Task<JokeModel?> GetByIdAsync(int id)
        {

            var joke = await _context.Jokes.FirstOrDefaultAsync(model => model.Id == id);
            return joke;
        }

        public async Task<JokeModel?> UpdateAsync(int id, JokeModel model)
        {
            if (!await ExistsByIdAsync(id))
            {
                return null;
            }
            model.Id = id;
            _context.Jokes.Update(model);
            await _context.SaveChangesAsync();
            await _context.Entry(model).ReloadAsync();
            return model;
        }


    }
}
