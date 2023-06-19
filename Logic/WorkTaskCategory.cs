using System.Text.Json.Serialization;

namespace Projekt_WPF_TODO_app
{
    public class WorkTaskCategory
    {
        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }

        [JsonPropertyName("name")]
        public int CategoryName { get; set; }
    }
}
