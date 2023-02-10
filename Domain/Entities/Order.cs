using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum OrderStatus
    {
        Started = 1,
        Finished = 9
    }
    public class Order
    {
        public Order(Guid id, OrderStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> Items { get; } = new List<OrderItem>();
    }
}
