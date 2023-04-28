using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Joke
{
    public class JokeUpdatePartially
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string? AuthorId { get; set; }

        public string? CategoryId { get; set; }

        public string? LanguageId { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public DateTime DateAdded { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}
