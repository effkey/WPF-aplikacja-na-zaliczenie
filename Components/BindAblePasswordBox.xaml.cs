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

namespace Projekt_WPF_TODO_app.Components
{
    /// <summary>
    /// Interaction logic for BindAblePasswordBox.xaml
    /// </summary>
    public partial class BindAblePasswordBox : UserControl
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindAblePasswordBox), new PropertyMetadata(string.Empty));

        public TextBlock TextPassword
        {
            get { return (TextBlock)GetValue(TextPasswordProperty); }
            set { SetValue(TextPasswordProperty, value); }
        }

        public static readonly DependencyProperty TextPasswordProperty =
            DependencyProperty.Register("TextPassword", typeof(TextBlock), typeof(BindAblePasswordBox), new PropertyMetadata(null));

        public BindAblePasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChnged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                TextPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextPassword.Visibility = Visibility.Visible;
            }
        }
    }
}
