using BestStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BestStoreMVC.Services
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<ProductModel> Products { get; set; }
    }
}
