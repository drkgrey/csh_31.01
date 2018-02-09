using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfTest_07_02
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        SqlDataAdapter adapterEmp;
        DataTable dt;
        DataTable db;
        SqlConnectionStringBuilder connectionString;
        SqlCommand command;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "07_02"
            };
            connection = new SqlConnection(connectionString.ConnectionString);
            adapter = new SqlDataAdapter();
            adapterEmp = new SqlDataAdapter();
            command = new SqlCommand("SELECT Id, DepartmentName FROM DepartmentsBase", connection);
            adapter.SelectCommand = command;
            dt = new DataTable();
            db = new DataTable();
            adapterEmp.SelectCommand = command;
            adapter.Fill(dt);
            DepartsList.ItemsSource = dt.DefaultView;
            CreateNewEmpDepCombobox.ItemsSource = dt.DefaultView;
            CreateNewEmpDepCombobox.DisplayMemberPath = "DepartmentName";
            NewDepComboBox.ItemsSource = dt.DefaultView;
            NewDepComboBox.DisplayMemberPath = "DepartmentName";
            command = new SqlCommand(@"UPDATE DepartmentsBase SET DepartmentName = @DepartmentName WHERE ID = @ID", connection);
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, -1, "DepartmentName");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;

            command = new SqlCommand(@"UPDATE EmployeeBase1 SET DepartmentId = @DepartmentId, Name= @Name, Lastname=@Lastname
                            WHERE ID = @ID", connection);
            command.Parameters.Add("@DepartmentId", SqlDbType.Int, -1, "DepartmentId");
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Lastname", SqlDbType.NVarChar, -1, "Lastname");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.UpdateCommand = command;

            command = new SqlCommand(@"INSERT INTO DepartmentsBase (DepartmentName)
                                        VALUES (@DepartmentName); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, -1, "DepartmentName");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            command = new SqlCommand($"DELETE FROM DepartmentsBase WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;

        }

        private void ChangeDep_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeList.SelectedIndex!=-1)
            NewDepComboBox.Visibility = Visibility.Visible;

        }

        private void NewDepComboBox_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeList.SelectedItem;
           
            if (NewDepComboBox.SelectedIndex != -1)
            {
                newRow.BeginEdit();
                newRow["DepartmentId"] = NewDepComboBox.SelectedIndex+1;
                newRow.EndEdit();
                adapterEmp.Update(db);
            }
            NewDepComboBox.Visibility = Visibility.Hidden;
        }

        private void AddDep_Click(object sender, RoutedEventArgs e)
        {
            
            DataRow newRow = dt.NewRow();

            if (CreateNewDepNameTextBox.Text != "")
            {
                newRow["DepartmentName"] = CreateNewDepNameTextBox.Text;
                dt.Rows.Add(newRow);
                adapter.Update(dt);
            }

        }

        private void IsCreateNewEmpCheck_Checked(object sender, RoutedEventArgs e)
        {
            CreateNewEmpNameTextBox.Visibility = Visibility.Visible;
            CreateNewEmpLastnameTextBox.Visibility = Visibility.Visible;
            CreateNewEmpDepCombobox.Visibility = Visibility.Visible;
        }

        private void IsCreateNewEmpCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            CreateNewEmpNameTextBox.Visibility = Visibility.Hidden;
            CreateNewEmpLastnameTextBox.Visibility = Visibility.Hidden;
            CreateNewEmpDepCombobox.Visibility = Visibility.Hidden;
            CreateNewEmpNameTextBox.Text = "";
            CreateNewEmpLastnameTextBox.Text = "";
            CreateNewEmpDepCombobox.SelectedIndex = -1;
        }
        private void CreateEmp()
        {
            command = new SqlCommand(@"INSERT INTO EmployeeBase1 (DepartmentId,Name,Lastname)
                                        VALUES (@DepartmentId,@Name, @Lastname); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@DepartmentId", SqlDbType.Int, -1, "DepartmentId");
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Lastname", SqlDbType.NVarChar, -1, "Lastname");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapterEmp.InsertCommand = command;
            
        }

        private void CreateNewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            db = new DataTable();
            adapterEmp.Fill(db);
            DataRow newRow = db.NewRow();
            if (CreateNewEmpDepCombobox.SelectedIndex != -1 && CreateNewEmpLastnameTextBox.Text != "" && CreateNewEmpNameTextBox.Text != "")
            {
                CreateEmp();
                newRow["DepartmentId"] = CreateNewEmpDepCombobox.SelectedIndex + 1;
                newRow["Name"] = CreateNewEmpLastnameTextBox.Text;
                newRow["Lastname"] = CreateNewEmpNameTextBox.Text;
                db.Rows.Add(newRow);
                adapterEmp.Update(db);
                EmployeeList.ItemsSource = db.DefaultView;
            }
            IsCreateNewEmpCheck_Unchecked(this, e);
        }

        private void DepartsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var c = DepartsList.SelectedIndex+1;
            if (c != 0)
                command = new SqlCommand($"SELECT Id,DepartmentId, Name,Lastname FROM EmployeeBase1 WHERE DepartmentId={c}", connection);
            adapterEmp.SelectCommand = command;
            db = new DataTable();
            adapterEmp.Fill(db);
            EmployeeList.ItemsSource = db.DefaultView;
        }

        private void DeleteDepButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartsList.SelectedIndex != -1)
            {
                DataRowView newRow = (DataRowView)DepartsList.SelectedItem;
                newRow.Row.Delete();
                adapter.Update(dt);
            }
        }

        private void DeleteEmpButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeList.SelectedIndex != -1)
            {
                command = new SqlCommand($"DELETE FROM EmployeeBase1 WHERE ID = @ID", connection);
                SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
                param.SourceVersion = DataRowVersion.Original;
                adapterEmp.DeleteCommand = command;
                DataRowView newRow = (DataRowView)EmployeeList.SelectedItem;
                newRow.Row.Delete();
                adapterEmp.Update(db);
            }
        }

        private void ChangeEmpInfo_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeList.SelectedItem;
            newRow.BeginEdit();
            newRow["Name"] = EmpNewDataTextBox.Text;
            newRow["Lastname"] = EmpNewLastnameTextBox.Text;
            newRow.EndEdit();
            adapterEmp.Update(db);
        }

        private void ChangeDepNameButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)DepartsList.SelectedItem;
            newRow.BeginEdit();
            newRow["DepartmentName"] = DepartChangeNameBox.Text;
            newRow.EndEdit();
            adapter.Update(dt);
        }
    }
}
