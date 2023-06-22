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

        SignIn signIn; 
        public SignInWindow(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            signIn = new SignIn(user);
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
            TxtBox.Text = passwordTextBox.Password;
            TxtBox1.Text = passwordTextBox1.Password;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordTextBox1.Visibility = Visibility.Collapsed;
            TxtBox.Visibility = Visibility.Visible;
            TxtBox1.Visibility = Visibility.Visible;
        }
        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Password = TxtBox.Text;
            passwordTextBox1.Password = TxtBox1.Text;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordTextBox1.Visibility = Visibility.Visible;
            TxtBox.Visibility = Visibility.Collapsed;
            TxtBox1.Visibility = Visibility.Collapsed;
        }
    }
}
