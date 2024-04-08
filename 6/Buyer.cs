using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6
{
    public class Buyer
    {
        public string Name { get; set; }

        public int ID
        {
            get { return id; }
            set { id = value < 0 ? 0 : value; }
        }

        public int CarID
        {
            get { return carID; }
            set { carID = value < 0 ? 0 : value; }
        }

        public Buyer(int id, string name, int carID)
        {
            ID = id;
            Name = name;
            CarID = carID;
        }

        private int id;
        private int carID;
    }
}
