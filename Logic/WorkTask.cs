using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekt_WPF_TODO_app.Logic
{
    public class WorkTask
    {
        [JsonPropertyName("task_id")]
        public int? TaskId { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }
        
        [JsonPropertyName("title")]
        public string? TaskTitle { get; set; }

        [JsonPropertyName("description")]
        public string? TaskDescription { get; set; }

        [JsonPropertyName("priority")]
        public string? TaskPriority { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime? TaskDueDate { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime? TaskStartDate { get; set; }

        [JsonPropertyName("completion_date")]
        public DateTime? TaskCompletionDate { get; set; }

        [JsonPropertyName("completed")]
        public bool? IsTaskComplited { get; set; }

    }
}
