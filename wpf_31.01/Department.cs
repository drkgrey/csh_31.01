using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_31._01
{
    class Department
    {
        public string name { get; set; }
        public List<Employee> listOfEmployees;

        public Department(string name)
        {
            this.name = name;
            listOfEmployees = new List<Employee>();
        }

        public void AddEmployee(Employee emp) =>  listOfEmployees.Add(emp);
        public void CreateList(int number)
        {
            for( int i =1; i < number+1; i++)
            {
                AddEmployee(new Employee($"name{i}", $"base_{name}",$"depart-{name}"));
            }
        }


    }
}
