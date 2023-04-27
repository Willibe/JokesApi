using AutoMapper;
using JokesApi.DTOs.Author;
using JokesApi.DTOs.Category;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Category;

namespace JokesApi.Services.Category
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<CategoryView> CreateAsync(CategoryCreate model)
        {
            try
            {
                var category = _mapper.Map<CategoryModel>(model);
                var categoryCreated = await _categoriesRepository.CreateAsync(category);
                var categoryView = _mapper.Map<CategoryView>(categoryCreated);
                return categoryView;
            }
            catch(AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<DuplicateIdentifierException>(ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await ValidateCategoryExistsAsync(id);
            return await _categoriesRepository.DeleteAsync(id);
        }

        public async Task<List<CategoryView>> GetAllAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();
            var categoriesViewList = _mapper.Map<List<CategoryView>>(categories);
            return categoriesViewList;
        }

        public async Task<CategoryView?> GetByIdAsync(int id)
        {
            await ValidateCategoryExistsAsync(id);
            var category = await _categoriesRepository.GetByIdAsync(id);
            var categoryView = _mapper.Map<CategoryView>(category);
            return categoryView;
        }

        public async Task<CategoryView?> GetByNameOrIdAsync(string nameOrId)
        {
            var category = await _categoriesRepository.GetByNameOrIdAsync(nameOrId);
            if (category == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.CategoryNotFound);
            }
            var categoryView = _mapper.Map<CategoryView>(category);
            return categoryView;
        }

        public async Task<CategoryView?> UpdateAsync(int id, CategoryUpdate model)
        {

            await ValidateCategoryExistsAsync(id);
            try
            {
                var category = _mapper.Map<CategoryModel>(model);
                var categoryUpdated = await _categoriesRepository.UpdateAsync(id, category);
                var categoryView = _mapper.Map<CategoryView>(categoryUpdated);
                return categoryView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<DuplicateIdentifierException>(ex);
            }
        }

        private async Task ValidateCategoryExistsAsync(int id)
        {
            if (!await _categoriesRepository.ExistsByIdAsync(id))
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.CategoryNotFound);
            }
        }
    }
}
