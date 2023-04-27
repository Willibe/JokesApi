using AutoMapper;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Category;

namespace JokesApi.Mapper.ValueProviders.Joke
{
    public class UpdatePartiallyCategoryIdResolver : IMemberValueResolver<object, object, string?, int>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public UpdatePartiallyCategoryIdResolver(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public int Resolve(object source, object destination, string? sourceMember, int destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return destMember;
            }
            var category = _categoriesRepository.GetByNameOrIdAsync(sourceMember).Result;
            if (category == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.CategoryNotFound);
            }
            return category.Id;
        }
    }
}
