using AutoMapper;
using JokesApi.DTOs.Category;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Category;

namespace JokesApi.Mapper.ValueProviders
{
    public class CategoryCreateIdResolver : IValueResolver<CategoryCreate, CategoryModel, string?>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoryCreateIdResolver(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public string Resolve(CategoryCreate source, CategoryModel destination, string? destMember, ResolutionContext context)
        {
            bool categoryExists = _categoriesRepository.ExistsByNameAsync(source.Name).Result;
            if (categoryExists)
            {
                throw new DuplicateIdentifierException(ErrorMessagesEnum.CategoryExists);
            }
            return source.Name;
        }
    }
}
