using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Services
{
    public class TableNotFoundException : Exception
    {
       
    }
    
    public interface ITableService 
    {
       Task <List<Table>> Get();
       Task Put(Table updatetable);
       Task Delete(Guid id);

        Task Post(Table table);
    }

    public class TableService : ITableService
    {
        public SmartBarDb db { get; set; }
        public TableService(SmartBarDb db) {

            this.db = db;
        }
        
        public async Task Delete(Guid id)
        {
            var table = await db.Tables.FindAsync(id);
            if (table is null)
            {
                throw new TableNotFoundException();
            }
            db.Tables.Remove(table);
            await db.SaveChangesAsync();
        }

        public async Task<List<Table>> Get()
        {
            var list = await db.Tables.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToListAsync();
            return list;
        }

        public async Task Put(Table updatetable)
        {
            var oldTable = await db.Tables.FindAsync(updatetable.Id);
            if (oldTable is null) throw new TableNotFoundException();
            if (updatetable.Name != null) oldTable.Name = updatetable.Name;
            if (updatetable.SortOrder != 0) oldTable.SortOrder = updatetable.SortOrder;
            await db.SaveChangesAsync();
        }

        public async Task Post(Table table)
        {
            await db.Tables.AddAsync(table);
            await db.SaveChangesAsync();        
        }
    }
}
