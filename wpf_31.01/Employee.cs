using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace wpf_31._01
{
    class Employee: INotifyPropertyChanged
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

        private string _lastName;
        public string Lastname
        {
            get => _lastName;
            set
            {
                if(_lastName != value)
                {
                    _lastName = value;
                    NotifyPropertyChanged("Lastname");
                }
            }
        }
        private string _dep;
        public string Dep
        {
            get => _dep;
            set
            {
                if (_dep != value)
                {
                    _dep = value;
                    NotifyPropertyChanged("Dep");
                }
            }
        }

        public Employee(string name, string lastName, string dep)
        {
            Name = name;
            Lastname = lastName;
            Dep= dep;
        }

        public Employee()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public override string ToString() => Name + " " + Lastname + " :" + Dep;

    }
}
