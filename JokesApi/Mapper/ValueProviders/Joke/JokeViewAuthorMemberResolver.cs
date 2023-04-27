using AutoMapper;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Author;

namespace JokesApi.Mapper.ValueProviders
{
    public class JokeViewAuthorMemberResolver : IMemberValueResolver<object, object, int, AuthorModel>
    {
        private readonly IAuthorsRepository _authorsRepository;
        public JokeViewAuthorMemberResolver(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public AuthorModel Resolve(object source, object destination, int sourceMember, AuthorModel destMember, ResolutionContext context)
        {
            var author = _authorsRepository.GetByIdAsync(sourceMember).Result;
            if (author == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.AuthorNotFound);
            }
            return author;
        }
    }
}
