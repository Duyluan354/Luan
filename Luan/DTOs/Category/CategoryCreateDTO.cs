using System.ComponentModel.DataAnnotations;

namespace Luan.DTOs.Category
{
    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        //public string UserCreate { get; set; }
    }
}
