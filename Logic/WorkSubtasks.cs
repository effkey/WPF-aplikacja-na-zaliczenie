using Projekt_WPF_TODO_app.Logic.Base;
using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app.Logic
{
    public class WorkSubtasks : BaseViewModel
    {
        
        public ObservableCollection<WorkSubtask> SubtasksList { get; set; } = new ObservableCollection<WorkSubtask>();

        public ObservableCollection<WorkSubtask> SubtasksDoneList { get; set; } = new ObservableCollection<WorkSubtask>();

        public ICommand DeleteSelectedSubtaskCommend { get; set; }
        public ICommand AddSelectedSubtaskToDoneListCommend { get; set; }

        public string SubTasksHeader { get; set; }

        public User user { get; set; }
        public WorkSubtasks(User user)
        {
            this.user = user;
            DeleteSelectedSubtaskCommend = new RelayCommand(AddSubTasksToDataBase);
            AddSelectedSubtaskToDoneListCommend = new RelayCommand(AddSelectedTaskskToDoneList);

        }

 
        private void DeleteSelectedTasks()
        {
            
/*
            foreach (var workTask in SubtasksList)
            {
                Console.WriteLine(workTask.ToString());
            }
*/
            var selectedTasks = SubtasksList.Where(x => x.IsSubtaskSelected).ToList();

            foreach (var task in selectedTasks)
            {
                SubtasksList.Remove(task);
            }
        }

        private void AddSelectedTaskskToDoneList()
        {
           /* foreach (var workTask in SubtasksList)
            {
                Console.WriteLine(workTask.ToString());
            }*/

            var selectedTasks = SubtasksList.Where(x => x.IsSubtaskSelected).ToList();


            foreach (var task in selectedTasks)
            {
                task.IsSubtaskCompleted = true;
                Console.WriteLine(task.ToString());
                SubtasksDoneList.Add(task);
                SubtasksList.Remove(task);
            }



        }

        public void AddSubTasks(WorkTask taskidobj)
        {
            ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
            Console.WriteLine("userid to jest to " + user.UserId);
            string response = apiHelper.SendGetRequest("/user-subtasks/" + user.UserId + "/");
            /*Console.WriteLine(response);*/
            List<WorkSubtask> tasks = JsonSerializer.Deserialize<List<WorkSubtask>>(response);
            foreach (WorkSubtask task in tasks)
            {
                Console.WriteLine(task);
               if(task.TaskId == taskidobj.TaskId)
                {         
                    if (task.IsSubtaskCompleted == true)
                    {
                        SubtasksDoneList.Add(task);
                    }
                    else
                    {
                        SubtasksList.Add(task);
                    }
                }
               

            }

        }

        public void AddTasksFromDataBase()
        {


            ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
            string response = apiHelper.SendGetRequest("/user-tasks/" + user.UserId);
            /*Console.WriteLine(response);*/
            List<WorkSubtask> tasks = JsonSerializer.Deserialize<List<WorkSubtask>>(response);

            foreach (WorkSubtask task in tasks)
            {
                if (task.isSubtaskTaskComplited == false)
                {
                    //Console.WriteLine(task);
                    SubtasksList.Add(task);
                }
                if (task.isSubtaskTaskComplited == true)
                {
                    //Console.WriteLine(task);
                    SubtasksDoneList.Add(task);
                }

            }
        }

        public List<WorkTask> Tasks { get; set; }
        public ObservableCollection<WorkSubtask> CombinedSubTasks { get; set; } = new ObservableCollection<WorkSubtask>();
        public void AddSubTasksToDataBase()
        {
            ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
            string response = apiHelper.SendDeleteRequest("/delete-all-subtasks/" + user.UserId, user.Token);
            // Połączenie dwóch list i przypisanie wyniku do CombinedTasks
            foreach (var task in SubtasksList)
            {
                CombinedSubTasks.Add(task);
            }

            foreach (var task in SubtasksDoneList)
            {
                CombinedSubTasks.Add(task);
            }

            var taskData = new
            {
                subtasks = CombinedSubTasks.Select(task => new
                {
                    id = task.SubtaskId,
                    task_id = task.TaskId,
                    description = task.SubtaskDescription,
                    completed = task.isSubtaskTaskComplited
                }).ToList()
            };


            ApiHelper apiHelper1 = new ApiHelper("http://kubpi.pythonanywhere.com");
            string jsonData = JsonSerializer.Serialize(taskData, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(jsonData);
            string response1 = apiHelper1.SendPostRequestWithHeaders(jsonData, "/add-subtasks/" + user.UserId + "/", user.Token);
            Console.WriteLine(response);
            CombinedSubTasks.Clear();

        }
    }
}
