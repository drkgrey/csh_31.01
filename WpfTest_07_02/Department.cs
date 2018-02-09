using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfTest_07_02
{
    public class Department
    { 
        public string DepartName { get; set; }
        public ObservableCollection<Employee> Employeeslist { get; set; }
        public override string ToString()
        {
            return DepartName;
        }
        public Department()
        {
        }
        public Department(string name,int nmbEmp)
        {
            DepartName = name;
            Employeeslist = new ObservableCollection<Employee>();
        }
        public Department(string name)
        {
            DepartName = name;
            Employeeslist = new ObservableCollection<Employee>();
            for (int i = 0; i < 5; i++)
            {
                AddEmp("name-" + i, "lastname-" + i);
            }
        }
        public void AddEmp(string Name, string Lastname)
        {
            Employeeslist.Add(new Employee(Name, Lastname, DepartName));
        }
        public void DeleteEmp(int index)
        {
            Employeeslist.RemoveAt(index);
        }
    }

    public class Departments
    {
        public ObservableCollection<Department> depsList { get; set; }

        public Departments(int count)
        {
            depsList = new ObservableCollection<Department>();
            for (int i = 1; i < count + 1; i++)
            {
                depsList.Add(new Department("department " + i));
            }
        }
        public Departments() : this(5)
        {

        }
    }
}
