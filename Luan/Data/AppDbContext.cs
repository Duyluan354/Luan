using Luan.Model;
using Microsoft.EntityFrameworkCore;

namespace Luan.Data
{
   
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
        }
    
}
