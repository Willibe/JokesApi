using AutoMapper;
using JokesApi.DTOs.Author;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Author;

namespace JokesApi.Mapper.ValueProviders
{
    public class AuthorUpdateIdResolver : IValueResolver<AuthorUpdate, AuthorModel, string?>
    {
        private readonly IAuthorsRepository _authorsRepository;
        public AuthorUpdateIdResolver(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public string Resolve(AuthorUpdate source, AuthorModel destination, string? destMember, ResolutionContext context)
        {
            bool authorExists = _authorsRepository.ExistsByNameAsync(source.Name).Result;
            if (authorExists)
            {
                throw new DuplicateIdentifierException(ErrorMessagesEnum.AuthorExists);
            }
            return source.Name;
        }
    }
}
