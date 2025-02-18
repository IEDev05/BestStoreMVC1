using BestStoreMVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using System.Configuration;
using BestStoreMVC.Services;
using Microsoft.EntityFrameworkCore;

namespace BestStoreMVC.Repository
{
    public class ProductcRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductcRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductModel>> GetAllProduct()
        {
            return await _context.Products.FromSqlRaw("Product_GetAllProduct").ToListAsync();

        }
        public async Task<bool> InsertProductAsync(ProductModel productModel)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "Product_InsertProduct @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                    productModel.Name,
                    productModel.Brand,
                    productModel.Category,
                    productModel.Price,
                    productModel.Description ?? (object)DBNull.Value,
                    productModel.ImageFileName ?? (object)DBNull.Value,
                    productModel.CreatedAt
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting product: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateProductAsync(ProductModel productModel)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC Product_UpdateProduct @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7",
                    productModel.Id,
                    productModel.Name,
                    productModel.Brand,
                    productModel.Category,
                    productModel.Price,
                    productModel.Description ?? (object)DBNull.Value,
                    productModel.ImageFileName ?? (object)DBNull.Value,
                    productModel.CreatedAt
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                return false;
            }
        }
        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var parameters = new[] { new SqlParameter("@Id", id) };

            var productList = await _context.Products
                .FromSqlRaw("EXEC Product_SelectForRecord @Id", parameters)
                .ToListAsync(); // Convert to list to fetch data properly

            return productList.FirstOrDefault(); // Get single product
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);

            int result = await _context.Database.ExecuteSqlRawAsync("Product_Delete @Id", parameter);

            return result > 0; // Returns true if deletion is successful
        }

    }
}
