using JokesApi.DataContext;
using JokesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesApi.Repositories.Category
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly JokesDataContext _context;

        public CategoriesRepository(JokesDataContext context)
        {
            _context = context;
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel model)
        {
            await _context.Categories.AddAsync(model);
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
            bool exists = await _context.Categories.AnyAsync(model => model.Id == id);
            return exists;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            bool exists = await _context.Categories.AnyAsync(model => model.Name == name);
            return exists;
        }

        public async Task<List<CategoryModel>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<CategoryModel?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(model => model.Id == id);
            return category;
        }

        public async Task<CategoryModel?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<CategoryModel?> GetByNameOrIdAsync(string id)
        {
            if (int.TryParse(id, out int idAsInt))
            {
                return await GetByIdAsync(idAsInt);
            }
            return await GetByNameAsync(id);
        }

        public async Task<CategoryModel?> UpdateAsync(int id, CategoryModel model)
        {
            if (!await ExistsByIdAsync(id))
            {
                return null;
            }
            model.Id = id;
            _context.Categories.Update(model);
            await _context.SaveChangesAsync();
            await _context.Entry(model).ReloadAsync();
            return model;
        }
    }
}
