using Projekt_WPF_TODO_app.Controls;
using Projekt_WPF_TODO_app.Logic;
using Projekt_WPF_TODO_app.Logic.Helpers;
using Projekt_WPF_TODO_app.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_WPF_TODO_app.Pages
{
    /// <summary>
    /// Interaction logic for WorkTaskPage.xaml
    /// </summary>
    public partial class WorkTaskPage : Page
    {
        MainWindow mainWindow;    
        WorkTasks workTask;
        Random random = new Random();
        public int rowIndex { get; set; }
        public User user { get; set; }
        WorkSubtasks workSubtasks;
        public WorkTaskPage(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            this.user = user;
            workTask = new WorkTasks(user);
            DataContext = workTask;
            workSubtasks = new WorkSubtasks(user);
            this.mainWindow = mainWindow;
            workTask.AddTasksFromDataBase();         
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
            dataGrid.ItemsSource = workTask.WorkTaskList;
            compitedTasks_checkbox.IsChecked = false;
            dataGrid.Columns[8].IsReadOnly = true;
            dataGrid.Columns[1].IsReadOnly = true;
            SubtaskTemplate.Visibility = Visibility.Hidden;
            SubtaskTemplate1.Visibility = Visibility.Visible;
        }

        private void AddEndEnableEditTask_UnChecked(object sender, RoutedEventArgs e)
        {
            // CheckBox został odznaczony, zablokuj kolumny
            SetColumnReadOnly(true);
            dataGrid.CanUserAddRows = false;
            dataGrid.ItemsSource = workTask.WorkTaskList;
            compitedTasks_checkbox.IsChecked = false;
            SubtaskTemplate1.Visibility = Visibility.Hidden;
            SubtaskTemplate.Visibility = Visibility.Visible;
        }
        private void ShowComplitedTasks_Checked(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = workTask.DoneTasks;
            completionDate.Visibility = Visibility.Visible;
            SetColumnReadOnly(true);        
        }
        private void ShowComplitedTasks_UnChecked(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = workTask.WorkTaskList;
            completionDate.Visibility = Visibility.Hidden;
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

        private void openSubtaskWindow(object sender, RoutedEventArgs e)
        {
            string titleHeader = "Subtaski zadania: " + workTask.ReturnTaskHeader(rowIndex);
          
            SubtasksWindow subtasksWindow = new SubtasksWindow(workTask.WorkTaskList[rowIndex], titleHeader, workSubtasks);
            subtasksWindow.ShowDialog();
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            // Pobierz zaznaczony wiersz
            var selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);


            if (selectedRow != null && workTask.WorkTaskList.Count > 0)
            {
                rowIndex = dataGrid.SelectedIndex;

                if (rowIndex >= 0 && rowIndex < workTask.WorkTaskList.Count)
                {
                    Console.WriteLine("Index wybranego wiersza: " + rowIndex);
                    Console.WriteLine("Wybrany element z WorkTaskList: " + workTask.WorkTaskList[rowIndex]);
                }
                else
                {
                    Console.WriteLine("Wybrany wiersz nie jest dostępny w WorkTaskList.");
                }
            }
            else
            {
                Console.WriteLine("Wybrany wiersz lub WorkTaskList jest null.");
            }

        }

        //private int nextTaskId = 1;
        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var newTask = new WorkTask();

            //Ikrementacja
          /*  newTask.TaskId = nextTaskId; // Set the TaskId for the new item
            nextTaskId++; // Increment the task ID for the next item
            e.NewItem = newTask;*/

            //Losowanie
            int randomNumber = random.Next(200, 10000);
            newTask.TaskId = randomNumber;
            e.NewItem = newTask;
        }

        //Tutaj trzeba zbindowac jakos przycisk zapisz.
        private void SaveTasks()
        {
            workTask.AddTasksToDataBase();
        }
    }
}
