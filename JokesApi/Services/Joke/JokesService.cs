using AutoMapper;
using JokesApi.DTOs.Joke;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Author;
using JokesApi.Repositories.Category;
using JokesApi.Repositories.Joke;
using JokesApi.Repositories.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JokesApi.Services.Joke
{
    public class JokesService : IJokesService
    {
        private readonly IJokesRepository _jokesRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ILanguagesRepository _languagesRepository;
        private readonly IMapper _mapper;

        public JokesService(
            IJokesRepository jokesRepository,
            ICategoriesRepository categoriesRepository,
            ILanguagesRepository languagesRepository,
            IMapper mapper)
        {
            _jokesRepository = jokesRepository;
            _categoriesRepository = categoriesRepository;
            _languagesRepository = languagesRepository;
            _mapper = mapper;
        }
        public async Task<JokeView> CreateAsync(JokeCreate model)
        {
            try
            {
                var jokeModel = _mapper.Map<JokeModel>(model);
                var createdJoke = await _jokesRepository.CreateAsync(jokeModel);
                var createdJokeView = _mapper.Map<JokeView>(createdJoke);
                return createdJokeView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<NoIdentifierFoundException>(ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await ValidateJokeExistsAsync(id);
            return await _jokesRepository.DeleteAsync(id);
        }

        public async Task<List<JokeView>> FilterByLanguageAsync(string langIdentifier)
        {
            var language = await _languagesRepository.GetByNameOrIdAsync(langIdentifier);
            var jokesViewList = new List<JokeView>();
            if (language == null)
            {
                return jokesViewList;
            }
            var jokesByLanguage = _jokesRepository.Filter(language.Id, "LanguageId");
            var jokesList = await jokesByLanguage.ToListAsync();
            jokesViewList = _mapper.Map<List<JokeModel>, List<JokeView>>(jokesList);
            return jokesViewList;
        }

        public async Task<List<JokeView>> FilterByLanguageAndCategoryAsync(string langIdentifier, string catIdentifier)
        {
            var language = await _languagesRepository.GetByNameOrIdAsync(langIdentifier);
            var category = await _categoriesRepository.GetByNameOrIdAsync(catIdentifier);
            var jokesViewList = new List<JokeView>();
            if (category == null || language == null)
            {
                return jokesViewList;
            }
            var jokesByLanguage = _jokesRepository.Filter(language.Id, "LanguageId");
            var jokesByCategory = _jokesRepository.Filter(category.Id, "CategoryId", jokesByLanguage);
            var jokesList = await jokesByCategory.ToListAsync();
            jokesViewList = _mapper.Map<List<JokeModel>, List<JokeView>>(jokesList);
            return jokesViewList;

        }

        public async Task<List<JokeView>> GetAllAsync()
        {
            var jokeList = await _jokesRepository.GetAllAsync();
            var jokeViewList = _mapper.Map<List<JokeView>>(jokeList);
            return jokeViewList;
        }

        public async Task<JokeView?> GetByIdAsync(int id)
        {
            await ValidateJokeExistsAsync(id);
            var joke = await _jokesRepository.GetByIdAsync(id);
            var jokeView = _mapper.Map<JokeView>(joke);
            return jokeView;
        }

        public async Task<JokeView?> UpdateAsync(int id, JokeUpdate model)
        {
            await ValidateJokeExistsAsync(id);
            try
            {
                var joke = _mapper.Map<JokeModel>(model);
                var updatedJoke = await _jokesRepository.UpdateAsync(id, joke);
                var updatedJokeView = _mapper.Map<JokeView>(updatedJoke);
                return updatedJokeView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<NoIdentifierFoundException>(ex);
            }

        }

        public async Task<JokeView> UpdatePartially(int id, JokeUpdatePartially jokeUpdatePartially)
        {
            await ValidateJokeExistsAsync(id);
            try
            {
                var jokeFromDb = await _jokesRepository.GetByIdAsync(id);
                var joke = _mapper.Map(jokeUpdatePartially, jokeFromDb);
                var updatedJokeModel = await _jokesRepository.UpdateAsync(id, joke);
                var jokeView = _mapper.Map<JokeView>(updatedJokeModel);
                return jokeView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<NoIdentifierFoundException>(ex);
            }
        }

        private async Task ValidateJokeExistsAsync(int id)
        {
            if (!await _jokesRepository.ExistsByIdAsync(id))
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.JokeNotFound);
            }
        }
    }
}
