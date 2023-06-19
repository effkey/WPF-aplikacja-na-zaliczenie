using System.Text.Json.Serialization;

namespace Projekt_WPF_TODO_app
{
    public class WorkSubtask
    {
        [JsonPropertyName("subtask_id")]
        public int SubtaskId { get; set; }

        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("completed")]
        public bool IsSubtaskCompleted { get; set; }
    }
}
