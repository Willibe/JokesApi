using JokesApi.DataContext;
using JokesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesApi.Repositories.Author
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly JokesDataContext _context;

        public AuthorsRepository(JokesDataContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorModel>> GetAllAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors;
        }

        public async Task<AuthorModel?> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(model => model.Id == id);
            return author;
        }

        public async Task<AuthorModel> CreateAsync(AuthorModel model)
        {
            model.RegistrationDate = DateTime.Now;
            await _context.Authors.AddAsync(model);
            await _context.SaveChangesAsync();
            await _context.Entry(model).ReloadAsync();
            return model;
        }

        public async Task<AuthorModel?> UpdateAsync(int id, AuthorModel model)
        {
            if (!await ExistsByIdAsync(id))
            {
                return null;
            }
            model.Id = id;
            _context.Authors.Update(model);
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
            model.Name = null;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool exists = await _context.Authors.AnyAsync(model => model.Id == id);
            return exists;
        }

        public async Task<AuthorModel?> GetByNameOrIdAsync(string id)
        {
            if (int.TryParse(id, out int idAsInt))
            {
                return await GetByIdAsync(idAsInt);
            }
            return await GetByNameAsync(id);
        }

        public async Task<AuthorModel?> GetByNameAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            bool exists = await _context.Authors.AnyAsync(model => model.Name == name);
            return exists;
        }
    }
}
