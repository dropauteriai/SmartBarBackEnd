using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MenuItem
    {
        public MenuItem(Guid id, string name, float price, int timesOrdered, int stock, Guid categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            TimesOrdered = timesOrdered;
            Stock = stock;
            CategoryId = categoryId;    
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; } 
        public int TimesOrdered { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }


    }
}
