namespace SmartBar.Controllers.DTO
{
    public class TableDTO
    {
        public TableDTO(int id, string name, int sortOrder)
        {
            Id = id;
            Name = name;
            SortOrder = sortOrder;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
    }

}
