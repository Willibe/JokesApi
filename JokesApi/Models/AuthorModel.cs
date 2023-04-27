using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.Models
{
    public class AuthorModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
