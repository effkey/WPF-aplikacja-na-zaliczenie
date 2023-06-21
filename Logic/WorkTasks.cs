using Projekt_WPF_TODO_app.Controls;
using Projekt_WPF_TODO_app.Logic.Base;
using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app
{
    public class WorkTasks : BaseViewModel
    {
        public ObservableCollection<WorkTask> WorkTaskList { get; set; } = new ObservableCollection<WorkTask>();

        public ObservableCollection<WorkTask> DoneTasks { get; set; } = new ObservableCollection<WorkTask>();

        public string NewWorkTaskTitle { get; set; }

        public string NewWorkTaskDescription { get; set; }

        public string NewWorkTaskPiority { get; set; }

        public DateTime NewWorkTaskDueDate { get; set; }

        public DateTime NewWorkTaskStartDate { get; set; }

        public DateTime NewWorkTaskComplitonDate { get; set; }

        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteSelectedTasksCommend { get; set; }
        public ICommand AddSelectedTaskskToDoneListCommend { get; set; }

        public WorkTasks()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommend = new RelayCommand(DeleteSelectedTasks);
            AddSelectedTaskskToDoneListCommend = new RelayCommand(AddSelectedTaskskToDoneList);
       
        }

        public void AddNewTask()
        {

            var newTask = new WorkTask
            {
                TaskTitle = NewWorkTaskTitle,
                TaskDescription = NewWorkTaskDescription,
                TaskPriority = null,
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

        private void AddSelectedTaskskToDoneList()
        {
            foreach (var workTask in WorkTaskList)
            {
                Console.WriteLine(workTask.ToString());
            }

            var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();


            foreach (var task in selectedTasks)
            {
                task.IsTaskComplited = true;
                Console.WriteLine(task.ToString());
                task.TaskCompletionDate = DateTime.Now;
                DoneTasks.Add(task);
                WorkTaskList.Remove(task);
            }
         

        }

        public void AddTasksFromDataBase()
        {
         
            ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
            string response = apiHelper.SendGetRequest("/user-tasks/1");
            /*Console.WriteLine(response);*/
            List<WorkTask> tasks = JsonSerializer.Deserialize<List<WorkTask>>(response);

            foreach (WorkTask task in tasks)
            {
                if (task.isTaskComplited == false)
                {
                    Console.WriteLine(task);
                    WorkTaskList.Add(task);
                }
                
            }
        }

        
    }
}
