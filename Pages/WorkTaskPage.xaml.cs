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
            this.mainWindow = mainWindow;
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            File.Delete("session.json");
            RestartApplication();
        }

        private void RestartApplication()
        {
            string? appPath = Environment.ProcessPath;
            Process.Start(appPath);
            Console.WriteLine("Wykonalem sie");
            Application.Current.Shutdown();
        }
    }
}
