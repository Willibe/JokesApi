namespace JokesApi.Models.CustomExceptionsModels
{
    public class NoIdentifierFoundException : Exception
    {
        public NoIdentifierFoundException(string message) : base(message) { }
    }
}
