using AutoMapper;
using JokesApi.DTOs.Joke;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Category;

namespace JokesApi.Mapper.ValueProviders
{
    public class JokeModelCategoryIdMemberResolver : IMemberValueResolver<object, object, string, int>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public JokeModelCategoryIdMemberResolver(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public int Resolve(object source, object destination, string sourceMember, int destMember, ResolutionContext context)
        {
            var category = _categoriesRepository.GetByNameOrIdAsync(sourceMember).Result;
            if (category == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.CategoryNotFound);
            }
            return category.Id;
        }
    }
}
