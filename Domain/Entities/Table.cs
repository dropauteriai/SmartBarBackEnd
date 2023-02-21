using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class Table
    {
        public Table(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
