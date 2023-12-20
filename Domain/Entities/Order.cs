using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Order(Guid id, Guid tableId, OrderStatus status, DateTime orderDate, string message)
        {
            Id = id;
            TableId = tableId;
            Status = status;
            OrderDate = orderDate;
            Message = message;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }

        public string Message { get; set; }

        public ICollection<OrderItem> Items { get; } = new List<OrderItem>();
    }
}
