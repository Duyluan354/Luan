using Luan.DTOs.Product;
using Luan.Model;

namespace Luan.Mapper
{
    public static class ProductMapper
    {
        // Chuyển từ DTO sang Model khi tạo mới
        public static Product ToProduct(ProductCreateDTO dto)
        {
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                UserCreate = "luan",
                UserDelete = "luan",
                UserUpdate = "luan",
                CreatedAt = DateTime.Now,
                CategoryId = dto.CategoryId
            };
        }
        public static void UpdateProduct(Product product, ProductUpdateDTO dto)
        {
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.UserUpdate = dto.UserUpdate;
            product.UserDelete = "luan";
            product.UserUpdate = "luan";
            product.UpdatedAt = DateTime.Now;
        }
        // Có thể mở rộng: Chuyển từ Product sang một DTO khác nếu cần
        // Ví dụ:
        // public static ProductResponseDTO ToProductResponseDTO(Product product)
        // {
        //     return new ProductResponseDTO
        //     {
        //         Id = product.Id,
        //         Name = product.Name,
        //         Price = product.Price,
        //         CategoryName = product.Category?.Name
        //     };
        // }
    }
}
