namespace SmartBar.Controllers.DTO
{
    public class PostTableRequest
    {
        public PostTableRequest(string name, int sortOrder)
        {
            Name = name;
            SortOrder = sortOrder;
        }

       
        public string Name { get; set; }
        public int SortOrder { get; set; }
    }

}
