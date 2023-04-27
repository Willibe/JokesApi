using AutoMapper;
using JokesApi.DTOs.Language;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Models;
using JokesApi.Repositories.Language;

namespace JokesApi.Mapper.ValueProviders
{
    public class LanguageUpdateIdResolver : IValueResolver<LanguageUpdate, LanguageModel, string?>
    {
        private readonly ILanguagesRepository _languageRepository;
        public LanguageUpdateIdResolver(ILanguagesRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public string? Resolve(LanguageUpdate source, LanguageModel destination, string? destMember, ResolutionContext context)
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
