﻿using Projekt_WPF_TODO_app.Logic.Base;
using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app
{
    public class WorkTasks : BaseViewModel
    {
        public ObservableCollection<WorkTask> WorkTaskList { get; set; } = new ObservableCollection<WorkTask>();

        public string NewWorkTaskTitle { get; set; }

        public string NewWorkTaskDescription { get; set; }

        public string NewWorkTaskPiority { get; set; }

        public DateTime NewWorkTaskDueDate { get; set; }

        public DateTime NewWorkTaskStartDate { get; set; }

        public DateTime NewWorkTaskComplitonDate { get; set; }

        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteSelectedTasksCommend { get; set; }

        public WorkTasks()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommend = new RelayCommand(DeleteSelectedTasks);
       
        }

        public void AddNewTask()
        {

            var newTask = new WorkTask
            {
                TaskTitle = NewWorkTaskTitle,
                TaskDescription = NewWorkTaskDescription,
                TaskPriority = NewWorkTaskPiority,
                TaskDueDate = NewWorkTaskDueDate,
                TaskStartDate = NewWorkTaskStartDate,
                //TaskCompletionDate = NewWorkTaskStartDate,
            };

            WorkTaskList.Add(newTask);
            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;

            OnPropertyChanged(nameof(NewWorkTaskTitle));
            OnPropertyChanged(nameof(NewWorkTaskDescription));
        }

        private void DeleteSelectedTasks()
        {
            foreach (var workTask in WorkTaskList)
            {
                Console.WriteLine(workTask.ToString());
            }

            var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

            foreach (var task in selectedTasks)
            {
                WorkTaskList.Remove(task);
            }
        }

        public void Add()
        {
            string dateString = "23.05.2023 00:00:00";
            string format = "dd.MM.yyyy HH:mm:ss";
            DateTime TaskDueDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);


            var newTask = new WorkTask
            {
                TaskTitle = "siemasiema",
                TaskDescription = "dupa123",
                TaskPriority = "taktak",
                TaskDueDate = null,
                TaskStartDate = null,
                //TaskCompletionDate = NewWorkTaskStartDate,
            };
            WorkTaskList.Add(newTask);
        }

        
    }
}
