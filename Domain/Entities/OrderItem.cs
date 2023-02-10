using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public OrderItem(string name, float price, int amount, bool delivered)
        {
            Name = name;
            Price = price;
            Amount = amount;
            Delivered = delivered;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public bool Delivered { get; set; }

    }
}
