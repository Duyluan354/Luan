﻿using Luan.Data;
using Luan.DTOs.Product;
using Luan.Mapper;
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
        public async Task<ActionResult<Product>> Create([FromBody] ProductCreateDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Dùng mapper để chuyển DTO thành entity
            var product = ProductMapper.ToProduct(productDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] ProductUpdateDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID trong URL không khớp với ID trong dữ liệu.");
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null || product.DeletedAt != null)
            {
                return NotFound("Không tìm thấy sản phẩm hoặc sản phẩm đã bị xóa.");
            }

            ProductMapper.UpdateProduct(product, dto);

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // DELETE: api/product/5?userDelete=admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đã xoá vĩnh viễn sản phẩm." });
        }
    }
}
