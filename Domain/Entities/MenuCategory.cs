using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MenuCategory
    {
        public MenuCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();
    }
}
