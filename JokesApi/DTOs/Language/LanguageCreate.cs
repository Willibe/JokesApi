using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Language
{
    public class LanguageCreate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
