using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Language
{
    public class LanguageUpdate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
