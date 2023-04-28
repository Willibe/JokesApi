using JokesApi.DTOs.Language;
using JokesApi.Models;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Joke
{
    public class JokeView
    {
        public int Id { get; set; }

        public AuthorModel Author { get; set; } = default!;

        public CategoryModel Category { get; set; } = default!;

        public LanguageView Language { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime DateAdded { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}
