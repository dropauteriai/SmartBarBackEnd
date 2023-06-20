using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
   public class SmartBarDb: DbContext
    {
        public SmartBarDb(DbContextOptions<SmartBarDb> options) : base(options)
        {
        }
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

    }
}