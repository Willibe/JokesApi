using AutoMapper;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Category;

namespace JokesApi.Mapper.ValueProviders
{
    public class JokeViewCategoryMemberResolver : IMemberValueResolver<object, object, int, CategoryModel>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public JokeViewCategoryMemberResolver(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public CategoryModel Resolve(object source, object destination, int sourceMember, CategoryModel destMember, ResolutionContext context)
        {
            var category = _categoriesRepository.GetByIdAsync(sourceMember).Result;
            if (category == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.CategoryNotFound);
            }
            return category;
        }
    }
}
