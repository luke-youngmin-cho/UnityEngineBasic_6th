using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Professor : Person
    {
        public int Salary { get; set; }

        public Professor(string name, int phoneNumber, string emailAddress)
            : base(name, phoneNumber, emailAddress)
        {
        }
    }
}
