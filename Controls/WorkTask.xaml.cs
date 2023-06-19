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

namespace Projekt_WPF_TODO_app.Controls
{
    /// <summary>
    /// Interaction logic for WorkTask.xaml
    /// </summary>
    public partial class WorkTask : UserControl
    {
        public WorkTask()
        {
            InitializeComponent();

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

        private void WorkTask_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("WorkTask clicked!");
        }




    }
}
