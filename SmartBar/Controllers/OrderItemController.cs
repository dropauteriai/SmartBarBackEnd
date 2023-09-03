using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SmartBar.Controllers.DTO;
using System.ComponentModel.Design;

namespace SmartBar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly SmartBarDb db;
        public OrderItemController(SmartBarDb db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<List<OrderItem>> Get(Guid tableId)
        {
            var order = await db.Orders.Where(t => t.Id == tableId).FirstOrDefaultAsync();//randam stalo orderi 
            if (order == null) return new List<OrderItem>();
            var orderId = order.Id;

            var list = await db.OrderItems.Where(t => t.OrderId == orderId).ToListAsync();//randam orderio itemus
            return list;
        }

        [HttpPost]
        public async Task<IResult> Post(Guid tableId, Guid menuId, int amount)
        {
            var menu = await db.Menus.FindAsync(menuId);
            if(menu == null)
                return Results.NotFound();
            var activeOrder = await db.Orders.Where(O => O.Status == OrderStatus.Started && tableId == O.Id).FirstOrDefaultAsync();
            if(activeOrder == null)
            {
                return Results.NotFound();
            }
            var orderItem = new OrderItem(Guid.NewGuid(), activeOrder.Id, menu.Name, menu.Price, amount, false);
            await db.OrderItems.AddAsync(orderItem);
            await db.SaveChangesAsync();
            return Results.Created($"/OrderItem/{orderItem.Id}", orderItem);
        }

        [HttpDelete]

        public async Task<IResult> Delete(Guid orderItemId)
        {
            var orderItem = await db.OrderItems.FindAsync(orderItemId);
            if (orderItem is null)
            {
                return Results.NotFound();
            }
            db.OrderItems.Remove(orderItem);
            await db.SaveChangesAsync();
            return Results.Ok();
        }

        [HttpPut]
        public async Task <IResult> Put (Guid id, int amount)
        {
          
            var oldOrderItem = await db.OrderItems.FindAsync(id);
            if(oldOrderItem == null)
            {
                return Results.NotFound();
            }
            
            if (amount != 0)
                oldOrderItem.Amount = amount;

            await db.SaveChangesAsync();
            return Results.Ok();
       
        }

    }

    
}
