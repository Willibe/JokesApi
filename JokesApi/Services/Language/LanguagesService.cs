using AutoMapper;
using JokesApi.DTOs.Author;
using JokesApi.DTOs.Category;
using JokesApi.DTOs.Language;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Language;

namespace JokesApi.Services.Language
{
    public class LanguagesService : ILanguagesService
    {
        private readonly ILanguagesRepository _languagesRepository;
        private readonly IMapper _mapper;

        public LanguagesService(ILanguagesRepository languagesRepository, IMapper mapper)
        {
            _languagesRepository = languagesRepository;
            _mapper = mapper;
        }

        public async Task<LanguageView> CreateAsync(LanguageCreate model)
        {
            try
            {
                var language = _mapper.Map<LanguageModel>(model);
                var languageCreated = await _languagesRepository.CreateAsync(language);
                var languageView = _mapper.Map<LanguageView>(languageCreated);
                return languageView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<DuplicateIdentifierException>(ex);
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            await ValidateLanguageExistsAsync(id);
            return await _languagesRepository.DeleteAsync(id);
        }

        public async Task<List<LanguageView>> GetAllAsync()
        {
            var languageList = await _languagesRepository.GetAllAsync();
            var languagesViewList = _mapper.Map<List<LanguageView>>(languageList);
            return languagesViewList;
        }

        public async Task<LanguageView?> GetByIdAsync(int id)
        {
            await ValidateLanguageExistsAsync(id);
            var language = await _languagesRepository.GetByIdAsync(id);
            var languageView = _mapper.Map<LanguageView>(language);
            return languageView;
        }

        public async Task<LanguageView?> GetByNameOrIdAsync(string nameOrId)
        {
            var language = await _languagesRepository.GetByNameOrIdAsync(nameOrId);
            if (language == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.LanguageNotFound);
            }
            var languageView = _mapper.Map<LanguageView>(language);
            return languageView;
        }

        public async Task<LanguageView?> UpdateAsync(int id, LanguageUpdate model)
        {
            await ValidateLanguageExistsAsync(id);
            try
            {
                var language = _mapper.Map<LanguageModel>(model);
                var languageUpdated = await _languagesRepository.UpdateAsync(id, language);
                var languageView = _mapper.Map<LanguageView>(languageUpdated);
                return languageView;
            }
            catch (AutoMapperMappingException ex) 
            { 
                throw MapperExceptionHelper.TryUnwrap<DuplicateIdentifierException>(ex); 
            }
        }

        private async Task ValidateLanguageExistsAsync(int id)
        {
            if (!await _languagesRepository.ExistsByIdAsync(id))
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.LanguageNotFound);
            }
        }
    }
}
