using AutoMapper;
using JokesApi.Models.CustomExceptionsModels;

namespace JokesApi.Helpers
{
    public static class MapperExceptionHelper
    {
        public static Exception TryUnwrap<TInnerException>(AutoMapperMappingException ex)
        {
            if (ex.InnerException != null && ex.InnerException.GetType() == typeof(TInnerException))
            {
                return ex.InnerException;
            }
            return ex;
        }
    }
}
