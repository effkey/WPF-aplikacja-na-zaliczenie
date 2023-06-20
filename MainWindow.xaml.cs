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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt_WPF_TODO_app.Pages;
using Projekt_WPF_TODO_app.Windows;

namespace Projekt_WPF_TODO_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindow mainwindow;

        SignIn signIn = new SignIn();
        LogIn login = new LogIn();
      
        public MainWindow()
        {
            InitializeComponent();
            mainwindow = this;
            if (login.ReadLogInSession())
            {
                ChangeIntoWorkTaskPage();
            }
        }

        private void SignIn_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SignInWindow window = new SignInWindow(signIn, mainwindow);
            window.ShowDialog();
        }

        private void LogIn_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LogInWindow window = new LogInWindow(login, mainwindow);
            window.ShowDialog();
        }
        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ResetPasswordWindow window = new ResetPasswordWindow(login, mainwindow);
            window.ShowDialog();
        }

        public void ChangeIntoWorkTaskPage()
        {
            this.Content = new WorkTaskPage(mainwindow);
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputEmail.Focus();
        }

        private void inputEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(inputEmail.Text) && inputEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputPassword.Focus();
        }

        private void inputPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(inputPassword.Password) && inputPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(inputEmail.Text) && !string.IsNullOrEmpty(inputPassword.Password))
            {
                MessageBox.Show("Pomyślnie zalogowano");
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)   // do zamykania aplikacji
        {
            Application.Current.Shutdown();
        }
    }
}
