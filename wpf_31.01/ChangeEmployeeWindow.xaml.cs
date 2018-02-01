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
    /// Логика взаимодействия для ChangeEmployeeWindow.xaml
    /// </summary>
    public partial class ChangeEmployeeWindow : Window
    {
        public ChangeEmployeeWindow()
        {
            InitializeComponent();
            
        }

        private void DepsBox_DropDownOpened(object sender, EventArgs e)
        {
            DepsBox.Items.Clear();
            NewDepComboBox.Items.Clear();
            EmplBox.Items.Clear();
            foreach (var c in MainWindow.listOfDeps)
            {
                DepsBox.Items.Add(c.name);
                NewDepComboBox.Items.Add(c.name);
            }
        }

        private void EmplBox_DropDownOpened(object sender, EventArgs e)
        {
            EmplBox.Items.Clear();
            if (DepsBox.SelectedIndex != -1)
            {foreach (var it in MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees)
                    EmplBox.Items.Add(it.name +" "+ it.lastName);
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmplBox.SelectedIndex != -1)
            {

                NameChangeBox.Visibility = Visibility.Visible;
                LastNameChangeBox.Visibility = Visibility.Visible;
                NewDepComboBox.Visibility = Visibility.Visible;
                NameChangeBox.Text = MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).name;
                LastNameChangeBox.Text = MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).lastName;
                NewDepComboBox.SelectedIndex = DepsBox.SelectedIndex;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmplBox_DropDownClosed(object sender, EventArgs e)
        {
            if (EmplBox.SelectedIndex != -1) BaseInfoLabel.Content = MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).GetInfo();
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).name = NameChangeBox.Text;
            MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).lastName = LastNameChangeBox.Text;
            if (MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.ElementAt(EmplBox.SelectedIndex).dep != MainWindow.listOfDeps.ElementAt(NewDepComboBox.SelectedIndex).name)
            {
                MainWindow.listOfDeps.ElementAt(DepsBox.SelectedIndex).listOfEmployees.RemoveAt(EmplBox.SelectedIndex);
                MainWindow.listOfDeps.ElementAt(NewDepComboBox.SelectedIndex).listOfEmployees.Add(new Employee(NameChangeBox.Text, LastNameChangeBox.Text, MainWindow.listOfDeps.ElementAt(NewDepComboBox.SelectedIndex).name));
            }
            NameChangeBox.Visibility = Visibility.Hidden;
            LastNameChangeBox.Visibility = Visibility.Hidden;
            NewDepComboBox.Visibility = Visibility.Hidden;
            DepsBox.Items.Clear();
            NewDepComboBox.Items.Clear();
            EmplBox.Items.Clear();
        }
    }
}
