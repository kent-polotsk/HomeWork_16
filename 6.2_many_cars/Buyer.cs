using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._2_many_cars
{
    internal class Buyer 
    {
        public List<int> cars;
        
        public string Name { get; set; }

        public int ID
        {
            get { return id; }
            set { id = value < 0 ? 0 : value; }
        }

        private int id;

        
        public Buyer(int id, string name) 
        {
            ID = id;
            Name = name;
            cars = new List<int>();
        }
    }
}
