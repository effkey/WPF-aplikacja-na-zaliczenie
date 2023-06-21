using Projekt_WPF_TODO_app.Logic.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Projekt_WPF_TODO_app
{
    public class WorkTask : BaseViewModel, INotifyPropertyChanged
    {
        [JsonPropertyName("id")]
        public int? TaskId { get; set; }

        [JsonPropertyName("user")]
        public int? UserId { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("title")]
        public string? TaskTitle { get; set; }

        [JsonPropertyName("description")]
        public string? TaskDescription { get; set; }

        [JsonPropertyName("priority")]
        public string TaskPriority { get; set; }
      
        [JsonPropertyName("due_date")]
        public DateTime? TaskDueDate { get; set; }

        [JsonIgnore]
        public string FormattedDueDate
        {
            get { return TaskDueDate?.ToString("yyyy-MM-dd HH:mm"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TaskDueDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
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
            get { return TaskStartDate?.ToString("yyyy-MM-dd HH:mm"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TaskStartDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
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
            get { return TaskCompletionDate?.ToString("yyyy-MM-dd HH:mm"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TaskCompletionDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                }
                else
                {
                    TaskCompletionDate = null;
                }
            }
        }

    
        public bool IsSelected { get; set; }


        public List<string> PriorityOptions { get; set; }

        public WorkTask()
        {
            // W konstruktorze lub metodzie inicjalizującej
            PriorityOptions = new List<string> { "High", "Medium", "Low" };

        }
        public override string ToString()
        {
            return $"Task Id: {TaskId}, User Id: {UserId}, Category: {Category}, Task Title: {TaskTitle}, Task Description: {TaskDescription}, Task Priority: {TaskPriority}, Task Due Date: {FormattedDueDate}, Task Start Date: {FormattedStartDate}, Task Completion Date: {FormattedCompletionDate}, Is Task Completed: {IsTaskComplited}, Is Selected: {IsSelected}";
        }

        [JsonPropertyName("completed")]
        private bool _isTaskComplited { get; set; }

        public bool IsTaskComplited
        {
            get { return _isTaskComplited; }
            set
            {
                if (_isTaskComplited != value)
                {
                    _isTaskComplited = value;
                    OnPropertyChanged(nameof(IsTaskComplited));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
