using System.ComponentModel.DataAnnotations;

namespace JokesApi.Models
{
    public class JokeModel
    {
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public int LanguageId { get; set; }

        public string? Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool Deleted { get; set; }
    }
}
