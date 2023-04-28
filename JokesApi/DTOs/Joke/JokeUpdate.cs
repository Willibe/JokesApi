using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Joke
{
    public class JokeUpdate
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string AuthorId { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string CategoryId { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string LanguageId { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; } = default!;

        [JsonIgnore]
        public DateTime DateAdded { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}
