using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Author
{
    public class AuthorUpdate
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = default!;

        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }
    }
}
