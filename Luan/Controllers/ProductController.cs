using Luan.Data;
using Luan.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Luan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .ToListAsync();
            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Tên sản phẩm là bắt buộc.");
            }

            if (product.Price <= 0)
            {
                return BadRequest("Giá sản phẩm phải lớn hơn 0.");
            }

            // Kiểm tra CategoryId có tồn tại trong cơ sở dữ liệu không
            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                return BadRequest("Danh mục không hợp lệ.");
            }

            // Gán thời gian tạo
            product.CreatedAt = DateTime.UtcNow;

            // Thêm sản phẩm vào cơ sở dữ liệu
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Trả về sản phẩm vừa tạo kèm theo mã trạng thái Created (201)
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.CategoryId = updatedProduct.CategoryId;
            product.UserUpdate = updatedProduct.UserUpdate;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE: api/product/5?userDelete=admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string userDelete)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            // Xoá mềm
            product.DeletedAt = DateTime.UtcNow;
            product.UserDelete = userDelete;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã xoá mềm sản phẩm." });
        }
    }
}
