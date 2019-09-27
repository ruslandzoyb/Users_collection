using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    [Serializable]
    public abstract class User
    {
        private string login;
        private string password;
        private string name;
        private string surname;
        private string otchestvo;
        private ushort age;
        private string sex;
        private string passport;
        public User()
        {
            Console.WriteLine("user");
        }

        public string Passport
        {
            get { return passport; }
        }
        public string Name
        {
            get { return name; }
           set
            {
                name = value;
            }
        }
    }

}