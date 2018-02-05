using System;
using System.Windows;
using System.Collections.ObjectModel;

namespace wpf_31._01
{
    /// <summary>
    /// Логика взаимодействия для ChangeDepartmentWindow.xaml
    /// </summary>
    public partial class ChangeDepartmentWindow : Window
    {
        internal ChangeDepartmentWindow(ObservableCollection<Department> ListOfDeps)
        {
            InitializeComponent();
            DepartsComboBox.ItemsSource = ListOfDeps;
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartsComboBox.SelectedIndex != -1 && NewDepNameTextBox.Text != "")
            {
                MainWindow window = (MainWindow)Owner;
                window.ListOfDeps[DepartsComboBox.SelectedIndex].Name = NewDepNameTextBox.Text;
                DepartsComboBox.Items.Refresh();
                DepartsComboBox.SelectedIndex = -1;
                NewDepNameTextBox.Text = "";
            }
            else MessageBox.Show("неверные данные");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateNewDep_Click_1(object sender, RoutedEventArgs e)
        {
            CreateDepartment createDepartment = new CreateDepartment();
            createDepartment.Owner = (MainWindow)Owner;
            createDepartment.ShowDialog();
        }
    }
}
