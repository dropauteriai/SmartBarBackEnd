using Domain.Entities;

namespace SmartBar.Controllers.DTO
{
    public class PostOrderRequest
    {
        public PostOrderRequest(string notes, List<AddOrderItemRequest> orderItems)
        {
            Notes = notes;
            OrderItems = orderItems;
        }

        public string Notes { get; set; }
        public List<AddOrderItemRequest> OrderItems { get; set; }
    }
}
