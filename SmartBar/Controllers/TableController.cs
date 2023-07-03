using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using SmartBar.Controllers.DTO;
using System.Runtime.CompilerServices;
using Table = Domain.Entities.Table;

namespace SmartBar.Controllers
{
    [ApiController]
    [EnableCors(nameof(AllowAnyCorsPolicy))]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly SmartBarDb db;
        public TableController(SmartBarDb db)
        {
            this.db = db;
        }

        [HttpGet]
        
        public async Task <List<Table>> Get()
        {
            //var tables = Enumerable.Range(1, 5).Select(index => new TableDTO(index, $"Stalas {index}", index)).ToList();
            //tables.Add(new TableDTO(100, "Baras", 100));
            //return tables;
            var list = await db.Tables.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToListAsync();
            return list; 
        }

        [HttpPost]
        public async Task<IResult> Post(PostTableRequest request)
        {
            var table = new Table(Guid.NewGuid(), request.Name, request.SortOrder);
            await db.Tables.AddAsync(table);
            await db.SaveChangesAsync();
            return Results.Created($"/table/{table.Id}", table);
        }

        [HttpPut]
        public async Task<IResult> Put(Table updatetable)
        {
            var oldTable = await db.Tables.FindAsync(updatetable.Id);
            if (oldTable is null) return Results.NotFound();
            if(oldTable.Name != null) oldTable.Name = updatetable.Name;
            if (oldTable.SortOrder != 0) oldTable.SortOrder = updatetable.SortOrder;
            await db.SaveChangesAsync();
            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> Delete(Guid id)
        {
            var table = await db.Tables.FindAsync(id);
            if (table is null)
            {
                return Results.NotFound();
            }
            db.Tables.Remove(table);
            await db.SaveChangesAsync();
            return Results.Ok();
        }

        

    }
    
}
