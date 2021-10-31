using Blazor.Domain.Models;
using Blazor.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProduct(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Products.FindAsync(id);

            return await context.Products
            .Include(p => p.ProductCategory)
             
              .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);

        }

        public void Remove(Product product)
        {
            context.Remove(product);
        }
    }
}
