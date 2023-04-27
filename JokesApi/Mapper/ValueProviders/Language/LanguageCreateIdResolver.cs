using AutoMapper;
using JokesApi.DTOs.Language;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Language;

namespace JokesApi.Mapper.ValueProviders
{
    public class LanguageCreateIdResolver : IValueResolver<LanguageCreate, LanguageModel, string?>
    {
        private readonly ILanguagesRepository _languageRepository;
        public LanguageCreateIdResolver(ILanguagesRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public string? Resolve(LanguageCreate source, LanguageModel destination, string? destMember, ResolutionContext context)
        {
            bool languageExists = _languageRepository.ExistsByNameAsync(source.Name).Result;
            if (languageExists)
            {
                throw new DuplicateIdentifierException(ErrorMessagesEnum.LanguageExists);
            }
            return source.Name;
        }
    }
}
