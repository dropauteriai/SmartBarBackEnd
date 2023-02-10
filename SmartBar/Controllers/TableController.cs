using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBar.Controllers.DTO;

namespace SmartBar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        public List<TableDTO> Get()
        {
            var tables = Enumerable.Range(1, 5).Select(index => new TableDTO(index, $"Stalas {index}", index)).ToList();
            tables.Add(new TableDTO(100, "Baras", 100));
            return tables;
        }

        /*public TableDTO Post(string tableName)
        {
            return;
        }*/
    }
    
}
