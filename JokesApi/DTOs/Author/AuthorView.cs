using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Author
{
    public class AuthorView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
