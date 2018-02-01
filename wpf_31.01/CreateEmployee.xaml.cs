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

namespace wpf_31._01
{
    /// <summary>
    /// Логика взаимодействия для CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        public CreateEmployee()
        {
            InitializeComponent();
            foreach (var c in MainWindow.listOfDeps)
                DepComboBox.Items.Add(c.name);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApproveButton__Click(object sender, RoutedEventArgs e)
        {
            if( NameBox.Text!="" && LastnameBox.Text!="" && DepComboBox.SelectedIndex != -1)
            {
                MainWindow.listOfDeps.ElementAt(DepComboBox.SelectedIndex).AddEmployee(new Employee(NameBox.Text, LastnameBox.Text, MainWindow.listOfDeps.ElementAt(DepComboBox.SelectedIndex).name));
                Close();
            }
        }
    }
}
