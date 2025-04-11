using Luan.DTOs.Category;
using Luan.Model;

namespace Luan.Mapper
{
    public static class CategoryMapper
    {
        public static Category ToCategory(CategoryCreateDTO dto)
        {
            return new Category
            {
                Name = dto.Name,
                //UserCreate = dto.UserCreate
            };
        }

        public static void UpdateCategory(Category category, CategoryUpdateDTO dto)
        {
            category.Name = dto.Name;
        }
    }
}
