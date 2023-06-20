﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
        WorkTasks workTask = new WorkTasks();
        public WorkTaskPage(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = workTask;
            this.mainWindow = mainWindow;
        
            workTask.Add();

        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Czy na pewno chcesz się wylogować?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                File.Delete("session.json");
                RestartApplication();
            }
        }

        private void RestartApplication()
        {
            string? appPath = Environment.ProcessPath;
            Process.Start(appPath);
            Console.WriteLine("Wykonalem sie");
            Application.Current.Shutdown();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // CheckBox został zaznaczony, odblokuj kolumny
            SetColumnReadOnly(false);
            SetDatePickerEnable(true);
            dataGrid.CanUserAddRows = true;

        }

        private void SetDatePickerEnable(bool isEnable)
        {
            var datePickers = FindVisualChildren<DatePicker>(dataGrid);
            foreach (var datePicker in datePickers)
            {
                if (datePicker.Tag is string tag && tag == "datePicker")
                {
                    datePicker.IsEnabled = isEnable;
                }
            }
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
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // CheckBox został odznaczony, zablokuj kolumny
            SetColumnReadOnly(true);
            SetDatePickerEnable(false);
            dataGrid.CanUserAddRows = false;


        }

        private bool isDueDateSortedAscending = true;

        private void SortByDueDate_Click(object sender, RoutedEventArgs e)
        {
            if (isDueDateSortedAscending)
            {
                dataGrid.Items.SortDescriptions.Clear();
                dataGrid.Items.SortDescriptions.Add(new SortDescription("TaskDueDate", ListSortDirection.Descending));
                SetDatePickerEnable(false);
            }
            else
            {
                dataGrid.Items.SortDescriptions.Clear();
                dataGrid.Items.SortDescriptions.Add(new SortDescription("TaskDueDate", ListSortDirection.Ascending));
                SetDatePickerEnable(false);
            }
            isDueDateSortedAscending = !isDueDateSortedAscending;
            ;
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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Ustawienie wiersza DataGrid jako tryb edycji
            var button = (Button)sender;
            var dataGridCell = FindVisualParent<DataGridCell>(button);
            if (dataGridCell != null)
            {
                dataGridCell.IsEditing = true;
                dataGridCell.Focus();
            }
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

    }
}
