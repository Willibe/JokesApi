using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Language
{
    public class LanguageUpdate
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = default!;
    }
}
