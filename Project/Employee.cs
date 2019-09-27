using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    public class Employee:User
    {

        private string company;
        private string position;
        private int salary;
        private int stage;
        public Employee() : base()
        {
              Console.WriteLine("Employee ");
        }
    }
}