using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Menu
    {
        public Menu(Guid id, string name, float price, int timesOrdered, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            TimesOrdered = timesOrdered;
            Stock = stock;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; } 
        public int TimesOrdered { get; set; }
        public int Stock { get; set; }


    }
}
