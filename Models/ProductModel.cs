namespace BestStoreMVC.Models
{
    public class ProductModel
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string ImageFileName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
