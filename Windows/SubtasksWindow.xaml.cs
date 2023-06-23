using Projekt_WPF_TODO_app.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_WPF_TODO_app.Windows
{
    /// <summary>
    /// Interaction logic for SubtasksWindow.xaml
    /// </summary>
    public partial class SubtasksWindow : Window
    {


        WorkSubtasks workSubtasks;
        WorkTask taskObject;
        Random random = new Random();
        public SubtasksWindow(WorkTask taskObject, string subtaskHeader, User user)
        {
            InitializeComponent();
            this.taskObject = taskObject;
            workSubtasks = new WorkSubtasks(user);
            DataContext = workSubtasks;
            workSubtasks.AddSubTasks(taskObject);
            workSubtasks.SubTasksHeader = subtaskHeader;
            //workTask.Add();

        }
        private void RestartApplication()
        {
            string? appPath = Environment.ProcessPath;
            Process.Start(appPath);
            Console.WriteLine("Wykonalem sie");
            Application.Current.Shutdown();
        }
        private void AddEndEnableEditTask_Checked(object sender, RoutedEventArgs e)
        {
            // CheckBox został zaznaczony, odblokuj kolumny
            SetColumnReadOnly(false);
            dataGrid.CanUserAddRows = true;
            dataGrid.ItemsSource = workSubtasks.SubtasksList;
            compitedTasks_checkbox.IsChecked = false;
            dataGrid.Columns[0].IsReadOnly = true;
            dataGrid.Columns[4].IsReadOnly = true;
        }
        private void AddEndEnableEditTask_UnChecked(object sender, RoutedEventArgs e)
        {
            // CheckBox został odznaczony, zablokuj kolumny
            SetColumnReadOnly(true);
            dataGrid.CanUserAddRows = false;
            dataGrid.ItemsSource = workSubtasks.SubtasksList;
            compitedTasks_checkbox.IsChecked = false;
        }
        private void ShowComplitedTasks_Checked(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = workSubtasks.SubtasksDoneList;
           
            SetColumnReadOnly(true);
        }
        private void ShowComplitedTasks_UnChecked(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = workSubtasks.SubtasksList;
          
        }
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    var child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child is T tChild)
                    {
                        yield return tChild;
                    }

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void SetColumnReadOnly(bool isReadOnly)
        {
            // Ustaw wartość IsReadOnly dla odpowiednich kolumn
            // Przyjmując, że pierwsza kolumna to DataGridTemplateColumn (CheckBox), indeksy są przesunięte o 1
            var columnCount = dataGrid.Columns.Count;
            for (int i = 1; i < columnCount; i++)
            {

                var column = dataGrid.Columns[i];
                column.IsReadOnly = isReadOnly;
            }



        }
        private void Logout_click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz się wylogować?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                File.Delete("session.json");
                RestartApplication();
            }
        }


        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var newSubtask = new WorkSubtask();
            newSubtask.TaskId = taskObject.TaskId; // Set the TaskId for the new item
            int randomNumber = random.Next(200, 10000);
            newSubtask.SubtaskId = randomNumber;
            e.NewItem = newSubtask;
        }
        
    }
}
