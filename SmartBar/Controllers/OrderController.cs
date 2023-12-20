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
   // [EnableCors(nameof(AllowAnyCorsPolicy))]
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
        public async Task<IResult> Post(Guid tableId, string notes)
        {
            //var orderId = Guid.NewGuid();
            var isThereOrder = await db.Orders.FindAsync(tableId);
            if (isThereOrder == null)
            {
                var order = new Order(Guid.NewGuid(), tableId, OrderStatus.Started, DateTime.Now, notes);
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                return Results.Created($"/Order/{order.Id}", order);
            }

            return Results.Accepted();
            //var items = new List<OrderItem>();
            /*foreach(var requestItem in request.OrderItems)
            {
                items.Add(new OrderItem(Guid.NewGuid(), orderId, requestItem.Name, 123, requestItem.Amount, false));
            }*/

           // await db.AddRangeAsync(items);
            
        }

        [HttpPut]
        public async Task<IResult> Put(Order updateOrder)
        {
            var order = await db.Orders.FindAsync(updateOrder.Id);
            if (order is null) return Results.NotFound();
            
            await db.SaveChangesAsync();
            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> Delete(Guid id)
        {
            var order = await db.Orders.FindAsync(id);
            if (order is null)
            {
                return Results.NotFound();
            }
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return Results.Ok();
        }



    }

}
