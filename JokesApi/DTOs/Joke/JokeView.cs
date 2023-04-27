using JokesApi.DTOs.Language;
using JokesApi.Models;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Joke
{
    public class JokeView
    {
        public int Id { get; set; }
        public AuthorModel Author { get; set; }
        public CategoryModel Category { get; set; }
        public LanguageView Language { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}
