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
        

        public MainWindow()
        {
            InitializeComponent();
            mainwindow = this;
          /*  UserLogged();*/
        }

        private void SignIn_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SignInWindow window = new SignInWindow(mainwindow, user);
            window.ShowDialog();
        }

        private void LogIn_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LogInWindow window = new LogInWindow(mainwindow, user);
            window.ShowDialog();
        }
        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
          
            ResetPasswordWindow window = new ResetPasswordWindow(mainwindow,user);
            window.ShowDialog();
        }

        public void ChangeIntoWorkTaskPage()
        {           
            this.Content = new WorkTaskPage(mainwindow, user.UserId);
        }

       /* public void UserLogged()
        {
            LogIn loginn = new LogIn(user);
            if (loginn.ReadLogInSession())
            {
                ChangeIntoWorkTaskPage();
            }
        }*/

    }
}
