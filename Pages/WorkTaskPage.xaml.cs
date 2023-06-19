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
        public WorkTaskPage(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new WorkTasks();
            this.mainWindow = mainWindow;
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

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // CheckBox został odznaczony, zablokuj kolumny
            SetColumnReadOnly(true);
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

    }
}
