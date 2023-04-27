using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Category
{
    public class CategoryCreate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
