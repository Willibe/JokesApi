using AutoMapper;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Language;

namespace JokesApi.Mapper.ValueProviders.Joke
{
    public class UpdatePartiallyLanguageIdResolver : IMemberValueResolver<object, object, string?, int>
    {
        private readonly ILanguagesRepository _languagesRepository;
        public UpdatePartiallyLanguageIdResolver(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }
        public int Resolve(object source, object destination, string? sourceMember, int destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return destMember;
            }
            var language = _languagesRepository.GetByNameOrIdAsync(sourceMember).Result;
            if (language == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.AuthorNotFound);
            }
            return language.Id;
        }
    }
}
