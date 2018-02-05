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
using System.Collections.ObjectModel;

namespace wpf_31._01
{
    /// <summary>
    /// Логика взаимодействия для ChangeEmployeeWindow.xaml
    /// </summary>
    public partial class ChangeEmployeeWindow : Window
    {
        public int c = -1;
        public int d = -1;
        internal ChangeEmployeeWindow(ObservableCollection<Department> ListOfDeps)
        {
            InitializeComponent();
            DepsBox.ItemsSource = ListOfDeps;
            NewDepComboBox.ItemsSource = ListOfDeps;
        }

        private void DepsBox_DropDownOpened(object sender, EventArgs e)
        {
           
        }

        private void EmplBox_DropDownOpened(object sender, EventArgs e)
        {
            c = DepsBox.SelectedIndex;
            var b = DepsBox.Items.GetItemAt(c) as Department;
            if (c != -1) EmplBox.ItemsSource = b.ListOfEmployees;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            d = EmplBox.SelectedIndex;
            if (EmplBox.SelectedIndex != -1)
            {
                var dd = EmplBox.SelectedItem as Employee;
                NameChangeBox.Visibility = Visibility.Visible;
                LastNameChangeBox.Visibility = Visibility.Visible;
                NewDepComboBox.Visibility = Visibility.Visible;
                NameChangeBox.Text = dd.Name;
                LastNameChangeBox.Text = dd.Lastname;
                NewDepComboBox.SelectedIndex = DepsBox.SelectedIndex;
                }
            }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmplBox_DropDownClosed(object sender, EventArgs e)
        {
          //  if (EmplBox.SelectedIndex != -1) BaseInfoLabel.Content = MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).GetInfo();
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Owner;
            window.ListOfDeps[c].ListOfEmployees[d].Name = NameChangeBox.Text;
            window.ListOfDeps[c].ListOfEmployees[d].Lastname = LastNameChangeBox.Text;
            if (window.ListOfDeps[c] != NewDepComboBox.SelectedItem)
            {
                window.ListOfDeps[c].ListOfEmployees.RemoveAt(EmplBox.SelectedIndex);
                window.ListOfDeps[NewDepComboBox.SelectedIndex].ListOfEmployees.Add(new Employee(NameChangeBox.Text, LastNameChangeBox.Text, window.ListOfDeps.ElementAt(NewDepComboBox.SelectedIndex).Name));
            }
            NameChangeBox.Visibility = Visibility.Hidden;
            LastNameChangeBox.Visibility = Visibility.Hidden;
            NewDepComboBox.Visibility = Visibility.Hidden;
            DepsBox.SelectedIndex = -1;
                NewDepComboBox.SelectedIndex = -1;
                EmplBox.SelectedIndex = -1;
            EmplBox.Items.Refresh();
        }
    }
}
