namespace Luan.DTOs.Product
{
    public class ProductCreateDTO
    {

        public string Name { get; set; }

        public decimal Price { get; set; }


        public string UserCreate { get; set; }


        // Khoá ngoại đến bảng Category
        public int CategoryId { get; set; }
        public string Description { get; internal set; }
        public int StockQuantity { get; internal set; }
        public string ImageUrl { get; internal set; }
        public string Brand { get; internal set; }
        public bool IsAvailable { get; internal set; }
        public double Rating { get; internal set; }
    }
}
