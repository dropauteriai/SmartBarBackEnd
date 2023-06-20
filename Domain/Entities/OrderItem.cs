﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public OrderItem( Guid id, string name, float price, int amount, bool delivered)
        {
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
            Delivered = delivered;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public bool Delivered { get; set; }

    }
}
