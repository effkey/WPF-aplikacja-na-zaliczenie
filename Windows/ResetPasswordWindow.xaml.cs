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
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {
        LogIn login;
        MainWindow mainWindow;
        User user;
        public ResetPasswordWindow( MainWindow mainWindow, User user)
        {
            InitializeComponent();
            DataContext = new ResetPassword();
            this.user = user;
         
            this.mainWindow = mainWindow;
            ((ResetPassword)DataContext).ResetInCompleted += ShowMassageBoxAfterReset;
        }
        private void ShowMassageBoxAfterReset(object? sender, bool success)
        {

            ResetPassword resetviewModel = (ResetPassword)DataContext;

            if (resetviewModel.IsResetSuccess)
            {
                this.Close();
                MessageBox.Show(resetviewModel.Message,"Success", MessageBoxButton.OK);
                LogInWindow window = new LogInWindow(mainWindow,user);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show(resetviewModel.Message,"Error", MessageBoxButton.OK);
            }
        }
    }
}
