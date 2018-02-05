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
    /// Логика взаимодействия для CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window
    {
        public CreateDepartment()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Owner;
            if (NameBox.Text != "") window.ListOfDeps.Add(new Department(NameBox.Text)); else MessageBox.Show("не указано имя");
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
