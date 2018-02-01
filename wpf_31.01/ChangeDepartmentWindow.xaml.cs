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
    /// Логика взаимодействия для ChangeDepartmentWindow.xaml
    /// </summary>
    public partial class ChangeDepartmentWindow : Window
    {
        public ChangeDepartmentWindow()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartsComboBox.SelectedIndex != -1 && NewDepNameTextBox.Text != "")
            {
                MainWindow.listOfDeps.ElementAt(DepartsComboBox.SelectedIndex).name = NewDepNameTextBox.Text;
                UpdateInfo();
                NewDepNameTextBox.Text = "";
            }
            else MessageBox.Show("неверные данные");
        }
        public void UpdateInfo()
        {
            DepartsComboBox.Items.Clear();
            foreach (var ob in MainWindow.listOfDeps) DepartsComboBox.Items.Add(ob.name);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateNewDep_Click_1(object sender, RoutedEventArgs e)
        {
            CreateDepartment createDepartment = new CreateDepartment();
            createDepartment.Owner = this;
            createDepartment.ShowDialog();
        }

        private void DepartsComboBox_DropDownOpened(object sender, EventArgs e)
        {
            UpdateInfo();
        }

    }
}
