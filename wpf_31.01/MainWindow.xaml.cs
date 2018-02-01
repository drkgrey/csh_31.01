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

namespace wpf_31._01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static List<Department> listOfDeps { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CreateDeps();
            UpdateInfo();
            
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
            listOfDeps = new List<Department>() { dep1, dep2, dep3, dep4 };
        }

        private void DepartsButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeDepartmentWindow changeDepartmentWindow = new ChangeDepartmentWindow();
            changeDepartmentWindow.Owner = this;
            changeDepartmentWindow.ShowDialog();

        }

        private void ShowEmployeesListButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList.Items.Clear();
            var gridView = new GridView();
            EmployeeList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Имя", DisplayMemberBinding = new Binding("name") });
            gridView.Columns.Add(new GridViewColumn { Header = "Фамилия", DisplayMemberBinding = new Binding("lastName") });
            gridView.Columns.Add(new GridViewColumn { Header = "Департамент", DisplayMemberBinding = new Binding("dep") });
            if (DepsList.SelectedIndex != -1)
            {
                var currentDep = listOfDeps.ElementAt(DepsList.SelectedIndex);
                foreach (var ob in currentDep.listOfEmployees)
                {
                    EmployeeList.Items.Add(ob);
                }
            }
            DepsList.SelectedIndex = -1;
        }
        public void UpdateInfo()
        {
            DepsList.Items.Clear();
            foreach (var ob in listOfDeps) DepsList.Items.Add(ob.name);
        }

        private void DepsList_DropDownOpened(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeEmployeeWindow changeEmployeeWindow = new ChangeEmployeeWindow();
            changeEmployeeWindow.ShowDialog();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee createEmployee = new CreateEmployee();
            createEmployee.ShowDialog();
        }
    }
}
