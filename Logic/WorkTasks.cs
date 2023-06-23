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
using System.Windows.Documents;
using System.Text.Json.Serialization;

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
        
            DeleteSelectedTasksCommend = new RelayCommand(AddTasksToDataBase);
            AddSelectedTaskskToDoneListCommend = new RelayCommand(AddSelectedTaskskToDoneList);     
        }
    
        private void DeleteSelectedTasks()
        {
           /* foreach (var workTask in WorkTaskList)
            {
                Console.WriteLine(workTask.ToString());
            }*/

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
               // Console.WriteLine(task.ToString());
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
                    //Console.WriteLine(task);
                    WorkTaskList.Add(task);
                }
                if (task.isTaskComplited == true)
                {
                    //Console.WriteLine(task);
                    DoneTasks.Add(task);
                }

            }
        }

        public List<WorkTask> Tasks { get; set; }
        public ObservableCollection<WorkTask> CombinedTasks { get; set; } = new ObservableCollection<WorkTask>();
        public void AddTasksToDataBase()
        {
            ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
            apiHelper.SendDeleteRequest("/delete-all-tasks/" + user.UserId, user.Token);
            apiHelper.SendDeleteRequest("/delete-all-subtasks/" + user.UserId, user.Token);


            // Połączenie dwóch list i przypisanie wyniku do CombinedTasks
            foreach (var task in WorkTaskList)
                {
                    CombinedTasks.Add(task);
                }

                foreach (var task in DoneTasks)
                {
                    CombinedTasks.Add(task);
                }

                var taskData = new
                {
                    tasks = CombinedTasks.Select(task => new
                    {
                        id = task.TaskId,
                        user = user.UserId,
                        category = task.Category,
                        title = task.TaskTitle,
                        description = task.TaskDescription,
                        priority = task.TaskPriority,
                        due_date = task.TaskDueDate?.ToString("yyyy-MM-dd"),
                        start_date = task.TaskStartDate?.ToString("yyyy-MM-dd"),
                        completion_date = task.TaskCompletionDate?.ToString("yyyy-MM-dd"),
                        completed = task.IsTaskComplited
                    }).ToList()
                };
     
           

            string jsonData = JsonSerializer.Serialize(taskData, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(jsonData);
            string response = apiHelper.SendPostRequestWithHeaders(jsonData, "/add-tasks/", user.Token);
            Console.WriteLine(response);
            CombinedTasks.Clear(); 
            
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
