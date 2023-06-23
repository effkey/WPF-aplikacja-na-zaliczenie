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
using System.Runtime.Intrinsics.X86;

namespace Projekt_WPF_TODO_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindow mainwindow;
        User user = new User();
        LogIn login;

        public MainWindow()
        {
            InitializeComponent();
            login = new LogIn(user, this);
            mainwindow = this;
            DataContext = login;

            if (login.ReadLogInSession())
            {
                ChangeIntoWorkTaskPage();
            }
            ((LogIn)DataContext).LogInCompleted += ShowMassageBoxAfterLogIn;
        }

        private void ShowMassageBoxAfterLogIn(object? sender, bool success)
        {

            LogIn loginviewModel = (LogIn)DataContext;

            if (loginviewModel.IsLogInSuccess)
            {
                MessageBox.Show("Pomyślnie zalogowano.", "Logowanie", MessageBoxButton.OK, MessageBoxImage.Information);
                ChangeIntoWorkTaskPage();
            }
            else
            {
                MessageBox.Show(loginviewModel.ErrorResponse, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignIn_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SignInWindow window = new SignInWindow(mainwindow, user);
            window.ShowDialog();
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
          
            ResetPasswordWindow window = new ResetPasswordWindow(mainwindow,user);
            window.ShowDialog();
        }

        public void ChangeIntoWorkTaskPage()
        {

            WorkTaskPage worktaskpageWindow = new WorkTaskPage(mainwindow, user);
            this.Close();
            worktaskpageWindow.ShowDialog();
        }

        /* public void UserLogged()
         {
             LogIn loginn = new LogIn(user);
             if (loginn.ReadLogInSession())
             {
                 ChangeIntoWorkTaskPage();
             }
         }*/

        private void textNick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputNick.Focus();
        }

        private void inputNick_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(inputNick.Text) && inputNick.Text.Length > 0)
            {
                textNick.Visibility = Visibility.Collapsed;
            }
            else
            {
                textNick.Visibility = Visibility.Visible;
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
            if(!string.IsNullOrEmpty(inputNick.Text) && !string.IsNullOrEmpty(inputPassword.Password))
            {
                MessageBox.Show("Pomyślnie zalogowano");
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)   // do zamykania aplikacji
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wyjść?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else if (result == MessageBoxResult.No)
            {

            }
        }

        private void textPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Sdadasda");
            //e.Handled = true; // Zatrzymuje propagację zdarzenia do BindAblePasswordBox
            inputPassword.Focus();
        }
    }
}
