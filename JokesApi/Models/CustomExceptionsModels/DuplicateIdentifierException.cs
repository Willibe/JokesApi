namespace JokesApi.Models.CustomExceptionsModels
{
    public class DuplicateIdentifierException : Exception
    {
        public DuplicateIdentifierException(string message) : base(message) { }
    }
}
