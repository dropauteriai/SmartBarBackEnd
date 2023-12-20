using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Cryptography.X509Certificates;

namespace SmartBar.Controllers
{
    
        [Route("[controller]")]
    [ApiController]
        public class MenuCategoryController : ControllerBase
        {
            private readonly SmartBarDb db;
            public MenuCategoryController(SmartBarDb db)
            {
                this.db = db;
            }

        [HttpGet]
            public async Task<List<MenuCategory>> Get()
            {
                var list = await db.MenuCategories.ToListAsync();
                return list;
            }

            [HttpDelete]
            public async Task<IResult> Delete(Guid MenuCategoryId)
            {
                var category = await db.MenuCategories.FindAsync(MenuCategoryId);
                if (category is null)
                {
                    return Results.NotFound();
                }
                db.MenuCategories.Remove(category);
                await db.SaveChangesAsync();
                return Results.Ok();
            }

            [HttpPost]
            public async Task<IResult> Post(string name)
            {
                var category = new MenuCategory(Guid.NewGuid(), name);
                if(category is null)
                {
                    return Results.NotFound();
                }
                await db.MenuCategories.AddAsync(category);
                await db.SaveChangesAsync();
                return Results.Created($"/MenuCategory/{category.Id}", category);

            }

            [HttpPut]
            public async Task<IResult> Put(MenuCategory update)
            {
                var oldMenuCategory = await db.MenuCategories.FindAsync(update.Id);

                if(oldMenuCategory is null) return Results.NotFound();

                if (update.Name != null) { oldMenuCategory.Name = update.Name; }

                await db.SaveChangesAsync();
                return Results.Ok();
            }
    }
}
