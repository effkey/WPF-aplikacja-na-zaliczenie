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
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        MainWindow mainWindow;
        public SignInWindow(SignIn signIn, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = signIn;
            this.mainWindow = mainWindow;
            ((SignIn)DataContext).SignInCompleted += ShowMassageBoxAfterSignIn;
        }

        private void ShowMassageBoxAfterSignIn(object? sender, bool success)
        {

            SignIn signinviewModel = (SignIn)DataContext;

            if (signinviewModel.IsSignInSuccess)
            {
                this.Close();
                MessageBox.Show(signinviewModel.Response);
                mainWindow.ChangeIntoWorkTaskPage();
            }
            else
            {
                MessageBox.Show(signinviewModel.Response);
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            textPassword.Text = inputPassword.Password;
            textPasswordRepeat.Text = inputPasswordRepeat.Password;
            inputPassword.Visibility = Visibility.Collapsed;
            inputPasswordRepeat.Visibility = Visibility.Collapsed;
            textPassword.Visibility = Visibility.Visible;
            textPasswordRepeat.Visibility = Visibility.Visible;
        }
        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            inputPassword.Password = textPassword.Text;
            inputPasswordRepeat.Password = textPasswordRepeat.Text;
            inputPassword.Visibility = Visibility.Visible;
            inputPasswordRepeat.Visibility = Visibility.Visible;
            textPassword.Visibility = Visibility.Collapsed;
            textPasswordRepeat.Visibility = Visibility.Collapsed;
        }

        private void textNick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputNick.Focus();
        }

        private void inputNick_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(inputNick.Text) && inputNick.Text.Length > 0)
            {
                textNick.Visibility = Visibility.Collapsed;
            }
            else
            {
                textNick.Visibility = Visibility.Visible;
            }
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputEmail.Focus();
        }

        private void inputEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(inputEmail.Text) && inputEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputPassword.Focus();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("blabla");
            this.Close();
        }
    }
}
