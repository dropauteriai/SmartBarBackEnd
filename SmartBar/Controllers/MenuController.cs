using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SmartBar.Controllers.DTO;

namespace SmartBar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly SmartBarDb db;
        public MenuController(SmartBarDb db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<List<MenuItem>> Get()
        {
            var list = await db.Menus.ToListAsync();
            return list;
        }

        [HttpPost]
        public async Task<IResult> Post(PostMenuRequest request)
        {
            var menu = new MenuItem(Guid.NewGuid(), request.Name, request.Price, 0, request.Stock) ;//pridedant menu elementa, jo bus uzsakyta 0 kartu
            await db.Menus.AddAsync(menu);
            await db.SaveChangesAsync();
            return Results.Created($"/Menu/{menu.Id}", menu);
        }

        [HttpDelete]

        public async Task<IResult> Delete(Guid MenuId)
        {
            var menu = await db.Menus.FindAsync(MenuId);
            if (menu is null)
            {
                return Results.NotFound();
            }
            db.Menus.Remove(menu);
            await db.SaveChangesAsync();
            return Results.Ok();
        }

        [HttpPut]
        public async Task <IResult> Put (MenuItem update)//id seno menu elemento, naujas vardas, nauja kaina, naujas kiek kartu uzsakyta
        {
            var oldMenu = await db.Menus.FindAsync(update.Id);
            if(oldMenu == null)
            {
                return Results.NotFound();
            }

            if (update.Price != 0)
                oldMenu.Price = update.Price;
            if (update.Name != null)
                oldMenu.Name = update.Name;
            if (update.TimesOrdered != 0)
                oldMenu.TimesOrdered = update.TimesOrdered;

            oldMenu.Stock = update.Stock;

            await db.SaveChangesAsync();
            return Results.Ok();
       
        }

    }

    
}
