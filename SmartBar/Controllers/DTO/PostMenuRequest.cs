namespace SmartBar.Controllers.DTO
{
    public class PostMenuRequest
    {

        public PostMenuRequest(string name, float price, int stock, Guid categoryId)
        {
            Name = name;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
