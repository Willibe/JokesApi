using AutoMapper;
using JokesApi.DTOs.Language;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Language;

namespace JokesApi.Mapper.ValueProviders
{
    public class JokeViewLanguageMemberResolver : IMemberValueResolver<object, object, int, LanguageView>
    {
        private readonly ILanguagesRepository _languagesRepository;
        private readonly IMapper _mapper;
        public JokeViewLanguageMemberResolver(ILanguagesRepository languagesRepository, IMapper mapper)
        {
            _languagesRepository = languagesRepository;
            _mapper = mapper;
        }

        public LanguageView Resolve(object source, object destination, int sourceMember, LanguageView destMember, ResolutionContext context)
        {
            var language = _languagesRepository.GetByIdAsync(sourceMember).Result;
            if (language == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.LanguageNotFound);
            }
            var languageView = _mapper.Map<LanguageView>(language);
            return languageView;
        }
    }
}
