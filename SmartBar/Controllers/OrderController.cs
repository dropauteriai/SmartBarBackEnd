using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using SmartBar.Controllers.DTO;
using System.Runtime.CompilerServices;
using Order = Domain.Entities.Order;

namespace SmartBar.Controllers
{
    [ApiController]
    [EnableCors(nameof(AllowAnyCorsPolicy))]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly SmartBarDb db;
        public OrderController(SmartBarDb db)
        {
            this.db = db;
        }

        [HttpGet]

        public async Task<List<Order>> Get()
        {
            //var Orders = Enumerable.Range(1, 5).Select(index => new OrderDTO(index, $"Stalas {index}", index)).ToList();
            //Orders.Add(new OrderDTO(100, "Baras", 100));
            //return Orders;
            var list = await db.Orders.ToListAsync();
            return list;
        }



        [HttpPost]
        public async Task<IResult> Post(OrderRequest request)
        {
            var orderId = Guid.NewGuid();
            var Order = new Order(orderId, OrderStatus.Started, DateTime.Now, request.Notes);
            var items = new List<OrderItem>();
            foreach(var requestItem in request.OrderItems)
            {
                items.Add(new OrderItem(Guid.NewGuid(), requestItem.Name, requestItem.Price, requestItem.Amount, false));
            }

            await db.Orders.AddAsync((Order)Order);
            await db.AddRangeAsync(items);
            await db.SaveChangesAsync();
            return Results.Created($"/Order/{Order.Id}", (object)Order);
        }

        [HttpPut]
        public async Task<IResult> Put(Order updateOrder)
        {
            var Order = await db.Orders.FindAsync(updateOrder.Id);
            if (Order is null) return Results.NotFound();
            
            await db.SaveChangesAsync();
            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> Delete(Guid id)
        {
            var Order = await db.Orders.FindAsync(id);
            if (Order is null)
            {
                return Results.NotFound();
            }
            db.Orders.Remove(Order);
            await db.SaveChangesAsync();
            return Results.Ok();
        }



    }

}
