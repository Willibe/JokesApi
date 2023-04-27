using AutoMapper;
using JokesApi.DTOs.Joke;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Language;

namespace JokesApi.Mapper.ValueProviders
{
    public class JokeModelLanguageIdMemberResolver : IMemberValueResolver<object, object, string, int>
    {
        private readonly ILanguagesRepository _languagesRepository;
        public JokeModelLanguageIdMemberResolver(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        public int Resolve(object source, object destination, string sourceMember, int destMember, ResolutionContext context)
        {
            var language = _languagesRepository.GetByNameAsync(sourceMember).Result;
            if (language == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.LanguageNotFound);
            }

            return language.Id;
        }
    }
}
