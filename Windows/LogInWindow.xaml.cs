using Projekt_WPF_TODO_app.Logic;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        MainWindow mainWindow;
        public LogInWindow(LogIn login, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = login;
            this.mainWindow = mainWindow;
            ((LogIn)DataContext).LogInCompleted += ShowMassageBoxAfterLogIn;
        }

        private void ShowMassageBoxAfterLogIn(object? sender, bool success)
        {

            LogIn loginviewModel = (LogIn)DataContext;

            if (loginviewModel.IsLogInSuccess)
            {
                this.Close();
                MessageBox.Show("Successfully logged in.", "Successfully log in", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.ChangeIntoWorkTaskPage();
            }
            else
            {
                MessageBox.Show(loginviewModel.ErrorResponse, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            TxtBox.Text = passwordTextBox.Password;
            passwordTextBox.Visibility = Visibility.Collapsed;
            TxtBox.Visibility = Visibility.Visible;
        }
        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Password = TxtBox.Text;
            passwordTextBox.Visibility = Visibility.Visible;
            TxtBox.Visibility = Visibility.Collapsed;
        }
    }
}