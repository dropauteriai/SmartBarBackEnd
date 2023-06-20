using Domain.Entities;

namespace SmartBar.Controllers.DTO
{
    public class OrderRequest
    {
        public OrderRequest(string notes, List<OrderItem> orderItems)
        {
            Notes = notes;
            OrderItems = orderItems;
        }

        public string Notes { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
