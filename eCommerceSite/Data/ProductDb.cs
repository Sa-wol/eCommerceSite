using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public static class ProductDb
    {
        /// <summary>
        /// Returns a total count of products
        /// </summary>
        /// <param name="_context">Database context to use</param>
        public async static Task<int> GetTotalProductsAsync(ProductContext _context)
        {
            return await(from p in _context.Products
                         select p).CountAsync();
        }

        /// <summary>
        /// Get a page worth of products
        /// </summary>
        /// <param name="_context">Database context to use</param>
        /// <param name="pageSize">The number of products per page</param>
        /// <param name="pageNum">Page of products to return</param>
        public async static Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        {
            return
               await (from p in _context.Products
                      orderby p.Title ascending
                      select p)
                      .Skip(pageSize * (pageNum - 1)) // skip() must be before Take()
                      .Take(pageSize)
                      .ToListAsync();
        }

        public async static Task<Product> AddProductAsync(ProductContext _context, Product p)
        {
            // Add to DB
            _context.Products.Add(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public static async Task<Product> GetProductAsync(ProductContext context, int prodId)
        {
            Product p = await (from products in context.Products
                               where products.ProductId == prodId
                               select products).SingleAsync();
            return p;
        }
    }
}
