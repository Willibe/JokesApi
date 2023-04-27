using AutoMapper;
using JokesApi.Helpers;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Author;
using JokesApi.Repositories.Joke;

namespace JokesApi.Mapper.ValueProviders.Joke
{
    public class UpdatePartiallyAuhorIdResolver : IMemberValueResolver<object, object, string?, int>
    {
        private readonly IAuthorsRepository _authorsRepository;

        public UpdatePartiallyAuhorIdResolver(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public int Resolve(object source, object destination, string? sourceMember, int destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return destMember;
            }
            var author = _authorsRepository.GetByNameOrIdAsync(sourceMember).Result;
            if (author == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.AuthorNotFound);
            }
            return author.Id;
        }
    }
}
