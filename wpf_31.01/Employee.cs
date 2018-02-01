using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_31._01
{
    class Employee
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string dep { get; set; }

        public Employee(string name, string lastName, string dep)
        {
            this.name = name;
            this.lastName = lastName;
            this.dep = dep;
        }

        public string GetInfo() => name + " " + lastName + " :" + dep;

       
    }
}
