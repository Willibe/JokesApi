using AutoMapper;
using JokesApi.DTOs.Joke;
using JokesApi.Helpers;
using JokesApi.Models;
using JokesApi.Models.CustomExceptionsModels;
using JokesApi.Repositories.Author;

namespace JokesApi.Mapper.ValueProviders
{
    public class JokeModelAuthorIdMemberResolver : IMemberValueResolver<object, object, string, int>
    {
        private readonly IAuthorsRepository _authorsRepository;
        public JokeModelAuthorIdMemberResolver(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }


        public int Resolve(object source, object destination, string sourceMember, int destMember, ResolutionContext context)
        {
            var author = _authorsRepository.GetByNameOrIdAsync(sourceMember).Result;
            if (author == null)
            {
                throw new NoIdentifierFoundException(ErrorMessagesEnum.AuthorNotFound);
            }

            return author.Id;
        }
    }
}
