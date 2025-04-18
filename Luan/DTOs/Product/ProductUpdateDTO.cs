using System.ComponentModel.DataAnnotations;

namespace Luan.DTOs.Product
{
    public class ProductUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }

        [Required]
        public string UserUpdate { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public string Description { get; internal set; }
        public int StockQuantity { get; internal set; }
        public string ImageUrl { get; internal set; }
        public string Brand { get; internal set; }
        public bool IsAvailable { get; internal set; }
        public double Rating { get; internal set; }
    }
}
