using AutoMapper;
using JokesApi.DTOs.Category;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Models;
using JokesApi.Repositories.Category;

namespace JokesApi.Mapper.ValueProviders
{
    public class CategoryUpdateIdResolver : IValueResolver<CategoryUpdate, CategoryModel, string?>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoryUpdateIdResolver(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public string Resolve(CategoryUpdate source, CategoryModel destination, string? destMember, ResolutionContext context)
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
