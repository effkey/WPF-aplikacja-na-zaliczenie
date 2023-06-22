﻿using Projekt_WPF_TODO_app.Logic.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app
{
    public class WorkSubtask : BaseViewModel, INotifyPropertyChanged
    {
     
        [JsonPropertyName("subtask_id")]
        public int SubtaskId { get; set; }

        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }

        [JsonPropertyName("description")]
        public string? SubtaskDescription { get; set; }

        [JsonPropertyName("completed")]

        public bool IsSubtaskSelected { get; set; }

        public bool isSubtaskTaskComplited { get; set; }

        public bool IsSubtaskCompleted
        {
            get { return isSubtaskTaskComplited; }
            set
            {
                if (isSubtaskTaskComplited != value)
                {
                    isSubtaskTaskComplited = value;
                    OnPropertyChanged(nameof(IsSubtaskCompleted));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override string ToString()
        {
            return $"Task SubtaskId: {SubtaskId}, Task Description: {SubtaskDescription}, Is Task Completed: {IsSubtaskCompleted}, Is Selected: {IsSubtaskSelected}";
        }
    }
}
