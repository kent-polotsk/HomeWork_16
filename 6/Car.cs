using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6
{
    public class Car
    {
        public string Name { get; set; }

        public int ID
        {
            get { return id; }
            set { id = value < 0 ? 0 : value; }
        }
        
        public int Age
        {
            get { return age; }
            set { age = value < 0 ? 0 : value; }
        }

        public Car(int id, string name, int age) 
        { 
            ID = id;
            Name = name;
            Age = age;
        }

        private int id;
        private int age;
    }
}
