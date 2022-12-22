using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Student : Person
    {
        public int StudentNum { get; set; }
        public char AverageMark { get; set; }
        public bool IsEligibleToEnroll
        {
            get
            {
                return AverageMark == 'A';
            }
        }

        private string[] _seminarsTaken;
        public bool TakenSeminar
        {
            get
            {
                return _seminarsTaken != null &&
                       _seminarsTaken.Length > 0;
            }
        }

        public Student(string name, int phoneNumber, string emailAddress) 
            : base(name, phoneNumber, emailAddress)
        {
        }

        public void TakeSeminars(string[] seminars)
        {
            _seminarsTaken = seminars;
        }
    }
}
