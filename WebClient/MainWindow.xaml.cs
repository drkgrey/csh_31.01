using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;


namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:64302/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        private async void GetButton_Click(object sender, RoutedEventArgs e)
        {
            var u = await client.GetAsync(client.BaseAddress + "api/DepartmentsBases");
            IEnumerable<Department> departments = await u.Content.ReadAsAsync<IEnumerable<Department>>();
            DepartsListView.ItemsSource = departments;
        }

        private async void DepartsListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string s = client.BaseAddress + $"api/EmployeeBase1/{DepartsListView.SelectedIndex+1}";
            var uu = await client.GetAsync(s);
            IEnumerable<Employee> employees = await uu.Content.ReadAsAsync<IEnumerable<Employee>>();
            EmployeeListView.ItemsSource = employees;
        }

        private async void SelectedDepartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage uuu = await client.GetAsync(client.BaseAddress + $"api/DepartmentsBases/{SelectedDepTextBox.Text}");
                Department depart = await uuu.Content.ReadAsAsync<Department>();
                if (SelectedDepTextBox.Text != "" && depart != null) MessageBox.Show(depart.DepartmentName + "-" + depart.Id);
            }
            catch(Exception)
            {
                MessageBox.Show("некорректные данные");
            }
           
            
        }

        private async void SelectedEmpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uuuu = await client.GetAsync(client.BaseAddress + $"api/EmployeeBase1/{EmpSearchTextBox.Text}");
                var emp = await uuuu.Content.ReadAsAsync<Employee>();
                if (EmpSearchTextBox.Text != "" && emp != null)
                    MessageBox.Show($"{emp.Name}  {emp.Lastname} - dep - {emp.DepartmentId}");
            }
            catch (Exception)
            {
                MessageBox.Show("некорректные данные");
            }
        }
    }
}
