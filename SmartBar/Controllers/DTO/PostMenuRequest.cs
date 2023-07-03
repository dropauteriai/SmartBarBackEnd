namespace SmartBar.Controllers.DTO
{
    public class PostMenuRequest
    {
        public PostMenuRequest(string name, float price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
