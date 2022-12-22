using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{

    internal class Person : IAttackable
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }
        public int PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }

        public Person(string name, int phoneNumber, string emailAddress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public void PurchaseParkingPass()
        {
            Console.WriteLine($"{Name} : 주차권 구매 완료");
        }

        public void Attack()
        {
            Console.WriteLine($"{Name} (이)가 공격했다.");
        }
    }
}
