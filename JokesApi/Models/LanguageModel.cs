using System.ComponentModel.DataAnnotations;

namespace JokesApi.Models
{
    public class LanguageModel
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
