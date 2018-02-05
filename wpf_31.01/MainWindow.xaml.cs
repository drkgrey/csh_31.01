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
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace wpf_31._01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Department> _listOfDeps;

        internal ObservableCollection<Department> ListOfDeps { get => _listOfDeps; set => _listOfDeps = value; }
        public MainWindow()
        {
            InitializeComponent();
            CreateDeps();
            DepsList.ItemsSource = ListOfDeps;
        }
        public void CreateDeps()
        {
            Department dep1 = new Department("department1");
            dep1.CreateList(4);
            Department dep2 = new Department("department2");
            dep2.CreateList(3);
            Department dep3 = new Department("department3");
            dep3.CreateList(4);
            Department dep4 = new Department("department4");
            dep4.CreateList(2);
            ListOfDeps = new ObservableCollection<Department>() { dep1, dep2, dep3, dep4 };
            var gridView = new GridView();
            EmployeeList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Имя", Width = 80, DisplayMemberBinding = new Binding("Name") });
            gridView.Columns.Add(new GridViewColumn { Header = "Фамилия", Width=120, DisplayMemberBinding = new Binding("Lastname") });
            gridView.Columns.Add(new GridViewColumn { Header = "Департамент", Width=120, DisplayMemberBinding = new Binding("Dep") });

        }

        private void DepartsButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeDepartmentWindow changeDepartmentWindow = new ChangeDepartmentWindow(ListOfDeps) { Owner= this
        };
            changeDepartmentWindow.Show();
            
        }

        private void ShowEmployeesListButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList.Items.Clear();
            if (DepsList.SelectedIndex != -1)
            {
                var currentDep = ListOfDeps.ElementAt(DepsList.SelectedIndex);
                foreach (var ob in currentDep.ListOfEmployees)
                {
                    EmployeeList.Items.Add(ob);
                }
            }
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeEmployeeWindow changeEmployeeWindow = new ChangeEmployeeWindow(ListOfDeps);
            changeEmployeeWindow.Owner = this;
            changeEmployeeWindow.ShowDialog();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee createEmployee = new CreateEmployee(ListOfDeps);
            createEmployee.Owner = this;
            createEmployee.ShowDialog();
        }

    }
}
