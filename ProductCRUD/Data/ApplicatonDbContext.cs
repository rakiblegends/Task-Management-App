using Microsoft.EntityFrameworkCore;
using ProductCRUD.Models;

namespace ProductCRUD.Data
{
    public class ApplicatonDbContext: DbContext
    {
        public ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
