using JokesApi.DataContext;
using JokesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesApi.Repositories.Language
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly JokesDataContext _context;
        public LanguagesRepository(JokesDataContext context)
        {
            _context = context;
        }

        public async Task<LanguageModel> CreateAsync(LanguageModel model)
        {
            await _context.Languages.AddAsync(model);
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
            bool exists = await _context.Languages.AnyAsync(model => model.Id == id);
            return exists;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            bool exists = await _context.Languages.AnyAsync(model => model.Name == name);
            return exists;
        }

        public async Task<List<LanguageModel>> GetAllAsync()
        {
            var categories = await _context.Languages.ToListAsync();
            return categories;
        }

        public async Task<LanguageModel?> GetByIdAsync(int id)
        {
            var category = await _context.Languages.FirstOrDefaultAsync(model => model.Id == id);
            return category;
        }

        public async Task<LanguageModel?> GetByNameAsync(string name)
        {
            return await _context.Languages.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<LanguageModel?> GetByNameOrIdAsync(string id)
        {
            if (int.TryParse(id, out int idAsInt))
            {
                return await GetByIdAsync(idAsInt);
            }
            return await GetByNameAsync(id);
        }

        public async Task<LanguageModel?> UpdateAsync(int id, LanguageModel model)
        {
            if (!await ExistsByIdAsync(id))
            {
                return null;
            }
            model.Id = id;
            _context.Languages.Update(model);
            await _context.SaveChangesAsync();
            await _context.Entry(model).ReloadAsync();
            return model;
        }
    }
}
