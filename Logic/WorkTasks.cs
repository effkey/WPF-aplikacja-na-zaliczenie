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
using Projekt_WPF_TODO_app.Logic;
namespace Projekt_WPF_TODO_app
{
    public class WorkTasks : BaseViewModel
    {
        public User user { get; set; }
        public ObservableCollection<WorkTask> WorkTaskList { get; set; } = new ObservableCollection<WorkTask>();

        public ObservableCollection<WorkTask> DoneTasks { get; set; } = new ObservableCollection<WorkTask>();

        public string NewWorkTaskTitle { get; set; }

        public string NewWorkTaskDescription { get; set; }

        public string NewWorkTaskPiority { get; set; }

        public string NewWorkTaskCategory { get; set; }

        public DateTime NewWorkTaskDueDate { get; set; }

        public DateTime NewWorkTaskStartDate { get; set; }

        public DateTime NewWorkTaskComplitonDate { get; set; }

        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteSelectedTasksCommend { get; set; }
        public ICommand AddSelectedTaskskToDoneListCommend { get; set; }

        public WorkTasks(User user)
        {
            this.user = user;
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommend = new RelayCommand(DeleteSelectedTasks);
            AddSelectedTaskskToDoneListCommend = new RelayCommand(AddSelectedTaskskToDoneList);
       
        }

        public void AddNewTask()
        {

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
            string response = apiHelper.SendGetRequest("/user-tasks/" + user.UserId);
            /*Console.WriteLine(response);*/
            List<WorkTask> tasks = JsonSerializer.Deserialize<List<WorkTask>>(response);

            foreach (WorkTask task in tasks)
            {
                if (task.isTaskComplited == false)
                {
                    Console.WriteLine(task);
                    WorkTaskList.Add(task);
                }
                if (task.isTaskComplited == true)
                {
                    Console.WriteLine(task);
                    DoneTasks.Add(task);
                }

            }
        }

        public void AddTasksToDataBase()
        {


           /* ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
             var json =  new 
            {
                user = 1,
                category = WorkTaskCategory
                 title = "amobus",
                 description = "Opis zadania 1",
                priority = "High",
                dueDate = DateTime.Parse("2023-06-30T12:00:00"),
                startDate = DateTime.Parse("2023-06-15T08:00:00"),
                completionDate = DateTime.Parse("2023-06-15T08:00:00"),
                completed = false,
                subtasks = new List<SubTask>
                {
                    new SubTask { Description = "pb", Completed = false },
                    new SubTask { Description = "pb", Completed = true }
                }
            };

            TaskData taskData = new TaskData
            {
                Tasks = new List<WorkTask> { task }
            };

            string jsonData = JsonSerializer.Serialize(taskData, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(jsonData);



            string response = apiHelper.SendPostRequestWithHeaders(json,"/user-tasks/" + user.UserId,user.Token);
            *//*Console.WriteLine(response);*//*
            List<WorkTask> tasks = JsonSerializer.Deserialize<List<WorkTask>>(response);

            foreach (WorkTask task in tasks)
            {
                if (task.isTaskComplited == false)
                {
                    Console.WriteLine(task);
                    WorkTaskList.Add(task);
                }
                if (task.isTaskComplited == true)
                {
                    Console.WriteLine(task);
                    DoneTasks.Add(task);
                }

            }*/
        }

        public string ReturnTaskHeader(int index)
        {
            if (WorkTaskList[index].TaskTitle != null)
            {
                return WorkTaskList[index].TaskTitle;
            }
            else
            {
                return "Brak tytulu";
            }
                 
        }

        public WorkTask ReturnTaskId(int index)
        {
            return WorkTaskList[index];
          
        }

    }
}
