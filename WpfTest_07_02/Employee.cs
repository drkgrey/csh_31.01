using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfTest_07_02
{
    public class Employee
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Depart { get; set; }
        

        public override string ToString()
        {
            return $"{Name} {Lastname} {Depart}";
        }
        public Employee(string name,string lastname, string depart)
        {
            Name = name;
            Lastname = lastname;
            Depart = depart;
        }
    }
}
