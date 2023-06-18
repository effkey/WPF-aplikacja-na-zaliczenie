using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekt_WPF_TODO_app.Logic
{
    public class WorkSubtask
    {
        [JsonPropertyName("subtask_id")]
        public int WorkSubtaskId { get; set; }

        [JsonPropertyName("task_id")]
        public int WorkSubtaskTaskId { get; set; }

        [JsonPropertyName("description")]
        public string WorkSubtaskDescription { get; set; }

        [JsonPropertyName("completed")]
        public bool IsSubtaskCompleted { get; set; }
    }
}
