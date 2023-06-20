using Projekt_WPF_TODO_app.Logic.Base;
using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Projekt_WPF_TODO_app
{
    public class WorkTask : BaseViewModel
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

        [JsonIgnore]
        public string FormattedDueDate
        {
            get { return TaskDueDate?.ToString("yyyy.MM.dd HH:mm"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TaskDueDate = DateTime.ParseExact(value, "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture);
                }
                else
                {
                    TaskDueDate = null;
                }
            }
        }

        [JsonPropertyName("start_date")]
        public DateTime? TaskStartDate { get; set; }

        [JsonIgnore]
        public string FormattedStartDate
        {
            get { return TaskStartDate?.ToString("yyyy.MM.dd HH:mm"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TaskStartDate = DateTime.ParseExact(value, "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture);
                }
                else
                {
                    TaskStartDate = null;
                }
            }
        }

        [JsonPropertyName("completion_date")]
        public DateTime? TaskCompletionDate { get; set; }

        [JsonIgnore]
        public string FormattedCompletionDate
        {
            get { return TaskCompletionDate?.ToString("yyyy.MM.dd HH:mm"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TaskCompletionDate = DateTime.ParseExact(value, "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture);
                }
                else
                {
                    TaskCompletionDate = null;
                }
            }
        }

        [JsonPropertyName("completed")]
        public bool? IsTaskComplited { get; set; }
        public bool IsSelected { get; set; }



        public override string ToString()
        {
            return $"Task Title: {TaskTitle}, Task Description: {TaskDescription}, TaskPriority: {TaskPriority}, TaskDueDate: {TaskDueDate} , TaskStartDate: {TaskStartDate}";
        }



    }
}
