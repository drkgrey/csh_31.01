using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;



namespace wpf_31._01
{
    class Department: INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name; 
            set
            {
                if (_name != value)
                    {
                        _name = value;
                        NotifyPropertyChanged("Name");
                    }
            }
        }
        private List<Employee> _listOfEmployees;
        public List<Employee> ListOfEmployees
        {
            get => _listOfEmployees; 
            set
            {
                if(_listOfEmployees != value)
                {
                    _listOfEmployees = value;
                    NotifyPropertyChanged("ListOfEmp");
                }
            }
        }


        public Department(string name)
        {
            Name = name;
            ListOfEmployees = new List<Employee>();
        }

        public Department()
        {
        }

        public void AddEmployee(Employee emp) =>  ListOfEmployees.Add(emp);
        public void CreateList(int number)
        {
            for( int i =1; i < number+1; i++)
            {
                AddEmployee(new Employee($"name{i}", $"base_{Name}",$"depart-{Name}"));
            }
        }
        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    
    }
}
