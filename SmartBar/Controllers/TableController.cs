using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using Services;
using SmartBar.Controllers.DTO;
using System.Runtime.CompilerServices;
using Table = Domain.Entities.Table;

namespace SmartBar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService tableService;
        public TableController(ITableService tableService)
        {
            this.tableService = tableService;
        }

        [HttpGet]
        
        public async Task <List<Table>> Get()
        {
            //var tables = Enumerable.Range(1, 5).Select(index => new TableDTO(index, $"Stalas {index}", index)).ToList();
            //tables.Add(new TableDTO(100, "Baras", 100));
            //return tables;
            return await tableService.Get();        }

        [HttpPost]
        public async Task<IResult> Post(PostTableRequest request)
        {
            var table = new Table(Guid.NewGuid(), request.Name, request.SortOrder);
            await tableService.Post(table);
            return Results.Created($"/table/{table.Id}", table);
        }

        [HttpPut]
        public async Task<IResult> Put(Table updatetable)
        {
            try
            {
                await tableService.Put(updatetable);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.NotFound();
            }
       
        }

        [HttpDelete]
        public async Task<IResult> Delete(Guid id)
        {
            try
            {
                await tableService.Delete(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.NotFound();
            }
        }

    }
    
}
