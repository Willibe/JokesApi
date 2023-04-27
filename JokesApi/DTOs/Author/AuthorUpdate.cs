using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Author
{
    public class AuthorUpdate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }
    }
}
