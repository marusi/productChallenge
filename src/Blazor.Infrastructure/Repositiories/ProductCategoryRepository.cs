
using Blazor.Domain.Models;
using Blazor.Domain.Services;
using Blazor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Repositories
{
   public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductCategory> GetProductCategory(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.ProductCategories.FindAsync(id);


            return await context.ProductCategories.SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Add(ProductCategory productCategory)
        {
            context.ProductCategories.Add(productCategory);

        }

        public void Remove(ProductCategory productCategory)
        {
            context.Remove(productCategory);
        }
    }
}
