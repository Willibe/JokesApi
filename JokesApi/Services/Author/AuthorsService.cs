using AutoMapper;
using JokesApi.DTOs.Author;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Author;

namespace JokesApi.Services.Author
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IMapper _mapper;
        public AuthorsService(IAuthorsRepository authorsRepository, IMapper mapper)
        {
            _authorsRepository = authorsRepository;
            _mapper = mapper;
        }

        public async Task<AuthorView> CreateAsync(AuthorCreate model)
        {
            try
            {
                var author = _mapper.Map<AuthorModel>(model);
                var authorCreated = await _authorsRepository.CreateAsync(author);
                var authorView = _mapper.Map<AuthorView>(authorCreated);
                return authorView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<DuplicateIdentifierException>(ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await ValidateAuthorExistsAsync(id);
            return await _authorsRepository.DeleteAsync(id);
        }

        public async Task<List<AuthorView>> GetAllAsync()
        {
            var authorList = await _authorsRepository.GetAllAsync();
            var authorsViewList = _mapper.Map<List<AuthorView>>(authorList);
            return authorsViewList;
        }

        public async Task<AuthorView?> GetByIdAsync(int id)
        {
            await ValidateAuthorExistsAsync(id);
            var author = await _authorsRepository.GetByIdAsync(id);
            var authorView = _mapper.Map<AuthorView>(author);
            return authorView;
        }

        public async Task<AuthorView?> GetByNameOrIdAsync(string nameOrId)
        {
            var author = await _authorsRepository.GetByNameOrIdAsync(nameOrId);
            if (author == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.AuthorNotFound);
            }
            var authorView = _mapper.Map<AuthorView>(author);
            return authorView;
        }

        public async Task<AuthorView?> UpdateAsync(int id, AuthorUpdate model)
        {

            await ValidateAuthorExistsAsync(id);
            try
            {
                var author = _mapper.Map<AuthorModel>(model);
                var authorUpdated = await _authorsRepository.UpdateAsync(id, author);
                var authorView = _mapper.Map<AuthorView>(authorUpdated);
                return authorView;
            }
            catch (AutoMapperMappingException ex)
            {
                throw MapperExceptionHelper.TryUnwrap<DuplicateIdentifierException>(ex);
            }

        }
        private async Task ValidateAuthorExistsAsync(int id)
        {
            if (!await _authorsRepository.ExistsByIdAsync(id))
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.AuthorNotFound);
            }
        }
    }
}
