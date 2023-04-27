using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Author
{
    public class AuthorCreate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }
    }
}
