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

        public WorkSubtasks()
        {
            DeleteSelectedSubtaskCommend = new RelayCommand(DeleteSelectedTasks);
            AddSelectedSubtaskToDoneListCommend = new RelayCommand(AddSelectedTaskskToDoneList);
        }

       

        private void DeleteSelectedTasks()
        {
            foreach (var workTask in SubtasksList)
            {
                Console.WriteLine(workTask.ToString());
            }

            var selectedTasks = SubtasksList.Where(x => x.IsSubtaskSelected).ToList();

            foreach (var task in selectedTasks)
            {
                SubtasksList.Remove(task);
            }
        }

        private void AddSelectedTaskskToDoneList()
        {
            foreach (var workTask in SubtasksList)
            {
                Console.WriteLine(workTask.ToString());
            }

            var selectedTasks = SubtasksList.Where(x => x.IsSubtaskSelected).ToList();


            foreach (var task in selectedTasks)
            {
                task.IsSubtaskCompleted = true;
                Console.WriteLine(task.ToString());
                SubtasksDoneList.Add(task);
                SubtasksList.Remove(task);
            }


        }

        public void AddDate(int rowIndex)
        {
            ApiHelper apiHelper = new ApiHelper("http://kubpi.pythonanywhere.com");
            string response = apiHelper.SendGetRequest("/user-subtasks/1/");
            /*Console.WriteLine(response);*/
            List<WorkSubtask> tasks = JsonSerializer.Deserialize<List<WorkSubtask>>(response);
            foreach (WorkSubtask task in tasks)
            {
               if(task.TaskId == rowIndex)
                {
                    SubtasksList.Add(task);
                }

            }

        }

        

    }
}
