using System.Text.Json.Serialization;

namespace JokesApi.DTOs.Category
{
    public class CategoryUpdate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
