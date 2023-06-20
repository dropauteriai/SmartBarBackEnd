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
        //public Table(string name, int sortOrder)
        //{
        //    Name = name;
        //    SortOrder = sortOrder;
        //}

        public Table(Guid id, string name, int sortOrder)
        {
            Id = id;
            Name = name;
            SortOrder = sortOrder;
            
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
